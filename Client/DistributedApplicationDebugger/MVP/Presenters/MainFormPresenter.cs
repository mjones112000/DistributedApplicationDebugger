using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using DistributedApplicationDebugger.Communication;
using DistributedApplicationDebugger.Settings;
using DistributedApplicationDebugger.Views;
using SSHLib;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using DistributedApplicationDebugger.Communication.MPICommands;

namespace DistributedApplicationDebugger.Presenters
{
  class MainFormPresenter
  {
    private SSHManager _connectionManager = null;
    private MainFormView _view = null;
    private SettingsManager _settingsManager = null;
    public event EventHandler<ValueEventArgs<string>> IncomingData;
    private StringBuilder _inputBuffer = new StringBuilder();
    private int _finalizedCount = 0;

    const string APP_NAME = "Client";

    public MainFormPresenter(MainFormView view, RedrawToolStrip playButtonToolStrip)
    {
      _view = view;
    }

    public SettingsManager Settings
    {
      get
      {
        if (_settingsManager == null)
        {
          ReadConfig();
        }
        return _settingsManager;
      }
    }

    public void Connect()
    {
      _finalizedCount = 0;

      _settingsManager.ConnectionLog = string.Empty;
      _settingsManager.RawStatusLog = string.Empty;

      UpdateSessionStatus(ConnectionStatus.Connecting);
      _settingsManager.SelectedSSHTabIndex = 1;


      DirectoryInfo fileDirectory = new DirectoryInfo(String.Format("{0}{1}{2}",
        Directory.GetCurrentDirectory(), LocalPathDelimiter, "DebuggerFiles").TrimEnd(LocalPathDelimiter.ToCharArray()));

      _connectionManager = new SSHManager(_settingsManager.SSHConfigurations, fileDirectory);


      _connectionManager.ConnectionComplete += new EventHandler<ValueEventArgs<bool>>(ConnectionComplete);
      _connectionManager.StatusUpdate += new EventHandler<ValueEventArgs<string>>(StatusUpdate);
      _connectionManager.RawDataUpdate += new EventHandler<ValueEventArgs<string>>(RawDataUpdate);

      _connectionManager.IncomingData += new EventHandler<ValueEventArgs<string>>(IncomingData);

      WriteConfig();

      new Thread(() => _connectionManager.Connect()).Start();

    }

    public void Disconnect()
    {
      if (_connectionManager != null)
      {
        _connectionManager.Disconnect();
      }

      Settings.ExecutableFolder = string.Empty;

      UpdateSessionStatus(ConnectionStatus.Idle);
    }

    public void ConnectInfoButtonClick()
    {
      Settings.IsConnectionInfoVisible = !Settings.IsConnectionInfoVisible;
    }

    public void AddSSHConfiguration()
    {
      SSHConfiguration sshConfiguration = _settingsManager.SSHConfigurations.AddNew();
      _settingsManager.SelectedConfiguration = sshConfiguration;
    }

    public void RemoveSSHConfiguration()
    {
      if (_settingsManager.SelectedConfiguration != null)
      {
        int index = _settingsManager.SSHConfigurations.IndexOf(_settingsManager.SelectedConfiguration);

        if (index > -1)
        {
          _settingsManager.SSHConfigurations.Remove(_settingsManager.SelectedConfiguration);
          _settingsManager.SelectedConfiguration = null;
        }

        if (Settings.SSHConfigurations.Count > 0)
        {
          if (Settings.SSHConfigurations.Count > index)
          {
            _settingsManager.SelectedConfiguration = _settingsManager.SSHConfigurations[index];
          }
          else
          {
            _settingsManager.SelectedConfiguration = _settingsManager.SSHConfigurations[Settings.SSHConfigurations.Count - 1];
          }
        }
      }
    }

    public void MessageBufferRequested(IEnumerable<IMessageCommand> messageCommands)
    {

      SSHConfiguration callCenterConfiguration = Settings.SSHConfigurations.Last();
      string sessionDir = String.Format("{0}/DebuggerFiles/Sessions", callCenterConfiguration.TransferDirectory).Trim('/');
      string fileLocation = string.Empty;
      if (Settings.ReplaySession != null)
      {

        fileLocation = String.Format("{0}/{1}/{2}/Node{3}.xml",
          sessionDir, Settings.SessionName, _settingsManager.ReplaySession.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"), messageCommands.First().NodeId);
      }
      else
      {
        fileLocation = String.Format("{0}/{1}/{2}/Node{3}.xml",
          sessionDir, Settings.SessionName, Settings.ReplaySessionName, messageCommands.First().NodeId);
      }

      _connectionManager.MessageBufferRequested(messageCommands, fileLocation, Settings.SOHReplacement, Settings.BARReplacement, Settings.EOTReplacement);
    }

    private void WriteConfig()
    {
      try
      {
        string s = ConfigFileLocation(APP_NAME);

        using (StreamWriter writer = new StreamWriter(ConfigFileLocation(APP_NAME), false))
        {
          writer.AutoFlush = true;
          XmlSerializer serializer = new XmlSerializer(typeof(SettingsManager));
          serializer.Serialize(writer, Settings);
          writer.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(String.Format(
          "An error occured while saving to the configuration file {0}{1}.",
          Environment.NewLine, ex.ToString()));
      }
    }

    public void MoveSSHConfigurationUp()
    {
      if (_settingsManager.SelectedConfiguration != null && _settingsManager.SelectedConfiguration != _settingsManager.SSHConfigurations[0])
      {
        int currentIndex = _settingsManager.SSHConfigurations.IndexOf(_settingsManager.SelectedConfiguration);

        SSHConfiguration currentConfiguration = _settingsManager.SelectedConfiguration;
        _settingsManager.SwapConfigurations(currentIndex, currentIndex - 1);
        _settingsManager.SelectedConfiguration = currentConfiguration;
      }
    }

    public void MoveSSHConfigurationDown()
    {
      if (_settingsManager.SelectedConfiguration != null && _settingsManager.SelectedConfiguration != _settingsManager.SSHConfigurations.Last())
      {
        int currentIndex = _settingsManager.SSHConfigurations.IndexOf(_settingsManager.SelectedConfiguration);
        SSHConfiguration currentConfiguration = _settingsManager.SelectedConfiguration;
        _settingsManager.SwapConfigurations(currentIndex, currentIndex + 1);
        _settingsManager.SelectedConfiguration = currentConfiguration;
      }
    }

    public void PlayButtonClicked(ToolStripSplitButton playButton, IList<int> gdbNodes)
    {
      
      if (playButton.ButtonPressed)
      {
        _finalizedCount = 0;

        _connectionManager.Play(_settingsManager.NodeCount, _settingsManager.HostFile,
          String.Format("{0}{1}", _settingsManager.ExecutableFolder, _settingsManager.ExecutableName).Trim(),
          _settingsManager.Params, gdbNodes, Settings.SOHReplacement, Settings.BARReplacement, Settings.EOTReplacement);

        UpdateSessionStatus(ConnectionStatus.Running);
      }
    }

    public void RecordButtonClicked(IList<int> gdbNodes)
    {
      _finalizedCount = 0;
      _connectionManager.Record(_settingsManager.NodeCount, _settingsManager.HostFile, 
        String.Format("{0}{1}", _settingsManager.ExecutableFolder, _settingsManager.ExecutableName).Trim(), _settingsManager.Params,
        gdbNodes, _settingsManager.SessionName, Settings.SOHReplacement, Settings.BARReplacement, Settings.EOTReplacement);


      UpdateSessionStatus(ConnectionStatus.Running);
    }

    public void MainFormClosed()
    {
      WriteConfig();
      Disconnect();
    }

    public void ReplayButtonClicked(IList<int> playbackNodes, IList<int> gdbNodes)
    {
      string playbackList = string.Empty;
      _finalizedCount = 0;

      _connectionManager.Replay(_settingsManager.NodeCount, _settingsManager.HostFile, 
        String.Format("{0}{1}", _settingsManager.ExecutableFolder, _settingsManager.ExecutableName).Trim(), 
        _settingsManager.Params, gdbNodes, _settingsManager.SessionName, 
        _settingsManager.ReplaySession.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"), playbackNodes,
        Settings.SOHReplacement, Settings.BARReplacement, Settings.EOTReplacement);

      UpdateSessionStatus(ConnectionStatus.Running);
    }

    public void GDBRequestIssued(int nodeId, string gdbMessage)
    {
      _connectionManager.GdbRequest(nodeId, gdbMessage);
    }

    private void ReadConfig()
    {
      if (File.Exists(ConfigFileLocation(APP_NAME)))
      {
        StreamReader input = null;
        try
        {
          input = new StreamReader(ConfigFileLocation(APP_NAME));
          XmlSerializer serializer = new XmlSerializer(typeof(SettingsManager));
          _settingsManager = serializer.Deserialize(input) as SettingsManager;
        }
        catch (Exception ex)
        {
          MessageBox.Show(String.Format(
            "An error occured while reading in the configuration file {0}{1}.",
            Environment.NewLine, ex.ToString()));
        }
        finally
        {
          if (input != null)
            input.Close();
        }
      }
      else
      {
        _settingsManager = new SettingsManager();
      }
    }

    private void UpdateStatus(string status)
    {
      if (_view.InvokeRequired)
      {
        _view.Invoke(new Action<string>(UpdateStatus), status);
        return;
      }

      _settingsManager.ConnectionLog = String.Format(
        "{0}{1}{2}", _settingsManager.ConnectionLog, Environment.NewLine, status).TrimStart(Environment.NewLine.ToCharArray());
    }

    private void UpdateSessionStatus(ConnectionStatus sessionStatus)
    {
      if (_view.InvokeRequired)
      {
        _view.Invoke(new Action<ConnectionStatus>(UpdateSessionStatus), sessionStatus);
        return;
      }

      _settingsManager.SessionStatus = sessionStatus;
    }

    private void UpdateRawData(string status)
    {
      if (_view.InvokeRequired)
      {
        _view.Invoke(new Action<string>(UpdateRawData), status);
        return;
      }

      _settingsManager.RawStatusLog = String.Format("{0}{1}", _settingsManager.RawStatusLog, status);
    }

    private void commManager_Disconnected(object sender, EventArgs e)
    {
      _connectionManager = null;
    }

    private void commManager_MessageRead(object sender, MessageReadEventArgs e)
    {
      string[] messageSplit = e.Message.Split(Protocal.PARTITION);
      int nodeId = 0;
      if (messageSplit.Length > 2 &&
        messageSplit.First().Equals(Protocal.SOH.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
        messageSplit.Last().Equals(Protocal.EOT.ToString(), StringComparison.CurrentCultureIgnoreCase)
        && Int32.TryParse(messageSplit[1], out nodeId))
      {
        //TODO Get from Node Panel Manager
        //if (_nodeDictionary.ContainsKey(nodeId))
        //{
        //  _nodeDictionary[nodeId].MessageRecieved(e.Message);
        //}
      }
    }

    private void StatusUpdate(object sender, ValueEventArgs<string> e)
    {
      UpdateStatus(e.Value);
    }

    private void RawDataUpdate(object sender, ValueEventArgs<string> e)
    {
      UpdateRawData(e.Value);
    }

    private void ConnectionComplete(object sender, ValueEventArgs<bool> e)
    {
      UpdateSessionStatus(e.Value ? ConnectionStatus.Connected : ConnectionStatus.Error);
    }

    public void SessionHistoryReceived(object sender, ValueEventArgs<string> e)
    {
      UpdateStatus("History List received\n");

      List<SessionInfo> sessions = new List<SessionInfo>();
      XmlSerializer serializer = new XmlSerializer(typeof(SessionInfo));

      foreach (string sessionXML in e.Value.Split(new string[]{"<SessionInfo>"}, StringSplitOptions.None))
      {
        try
        {
          if (String.IsNullOrEmpty(sessionXML))
            continue;

          MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(String.Format("<SessionInfo>{0}", sessionXML)));
          sessions.Add(serializer.Deserialize(memoryStream) as SessionInfo);
          memoryStream.Close();
        }
        catch (InvalidOperationException ex)
        {
          UpdateStatus(String.Format("Misformatted history message{0}{1}{0}{2}", Environment.NewLine, ex.ToString(), sessionXML));
        }
      } 

      List<ToolStripMenuItem> sessionButtons = new List<ToolStripMenuItem>();
      
      foreach (var session in sessions.GroupBy(x => x.SessionName).OrderBy(x=>x.Key))
      {
        ToolStripMenuItem mainMenu = new ToolStripMenuItem(session.Key);
        foreach (var sessionInstance in session.OrderByDescending(x=>x.StartTime))
        {
          ToolStripMenuItem sessionMenuItem = new ToolStripMenuItem(sessionInstance.StartTime.ToString());
          sessionMenuItem.Tag = sessionInstance;
          sessionMenuItem.Click +=new EventHandler(SessionSelected);
          mainMenu.DropDownItems.Add(sessionMenuItem);
        }
        sessionButtons.Add(mainMenu);

      }

      UpdateHistoryList(sessionButtons);
      UpdateSessionStatus(ConnectionStatus.Connected);
    }

    public void RecordSessionReceived(string sessionInfo)
    {
      UpdateStatus("Record Session Data received\n");

      SessionInfo session = new SessionInfo();
      XmlSerializer serializer = new XmlSerializer(typeof(SessionInfo));

      try
      {
        MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(sessionInfo));
        session = serializer.Deserialize(memoryStream) as SessionInfo;
        memoryStream.Close();

        AddSessionToHistory(session);
      }
      catch (InvalidOperationException ex)
      {
        UpdateStatus(String.Format("Misformatted history message{0}{1}{0}{2}", Environment.NewLine, ex.ToString(), sessionInfo));
      }
    }

    private void AddSessionToHistory(SessionInfo sessionInfo )
    {
      if (_view.InvokeRequired)
      {
        _view.Invoke(new Action<SessionInfo>(AddSessionToHistory), sessionInfo);
        return;
      }

      ToolStripMenuItem mainMenu = Settings.ReplayButtons.FirstOrDefault(x => x.Text == sessionInfo.SessionName);
      if (mainMenu == null)
      {
        mainMenu = new ToolStripMenuItem(sessionInfo.SessionName);
        ToolStripMenuItem proceedingMenu = Settings.ReplayButtons.FirstOrDefault(x => x.Text.CompareTo(mainMenu.Text) > 0);
        if (proceedingMenu == null)
        {
          Settings.ReplayButtons.Add(mainMenu);
        }
        else
        {
          Settings.ReplayButtons.Insert(Settings.ReplayButtons.IndexOf(proceedingMenu), mainMenu);
        }
      }

      ToolStripMenuItem sessionMenuItem = new ToolStripMenuItem(sessionInfo.StartTime.ToString());
      sessionMenuItem.Tag = sessionInfo;
      sessionMenuItem.Click += new EventHandler(SessionSelected);
      mainMenu.DropDownItems.Insert(0,sessionMenuItem);
    }

    private void UpdateHistoryList(List<ToolStripMenuItem> sessionButtons)
    {
      if (_view.InvokeRequired)
      {
        _view.Invoke(new Action<List<ToolStripMenuItem>>(UpdateHistoryList), sessionButtons);
        return;
      }

      Settings.ReplayButtons.Clear();
      sessionButtons.ForEach(x => Settings.ReplayButtons.Add(x));

      _view.PlayButton.DropDown.Refresh();
    }

    public void FileNotFoundDetected(object sender, ValueEventArgs<string> e)
    {
      if (_view.InvokeRequired)
      {
        _view.Invoke(new Action<object, ValueEventArgs<string>>(FileNotFoundDetected), sender, e);
        return;
      }

      MessageBox.Show(String.Format("File {0} not found.", e.Value), "Invalid Execution Path");
      UpdateSessionStatus(ConnectionStatus.Connected);
    }

    public void FinalizedCountUpdate(object sender, ValueEventArgs<int> e)
    {
      _finalizedCount = _finalizedCount + e.Value;
      if (_finalizedCount == _settingsManager.NodeCount)
      {
        _connectionManager.SessionComplete();
        UpdateSessionStatus(ConnectionStatus.Complete);
      }
    }

    private void SessionSelected(object sender, EventArgs e)
    {
      ToolStripItem toolStrip = sender as ToolStripItem;
      if (toolStrip != null)
      {
        Settings.ReplaySession = toolStrip.Tag as SessionInfo;
      }
    }

    private string LocalPathDelimiter
    {
      get
      {
        if (Environment.CurrentDirectory.Contains("\\"))
          return "\\";
        else
          return "/";
      }
    }

    private static string ConfigFileLocation(string applicationName)
    {
      char directorySeprator = '/';
      if (IsWindowsOS)
        directorySeprator = '\\';

      return String.Format("{0}{1}{2}.configfile", Application.StartupPath, directorySeprator, applicationName);
    }

    private static bool IsWindowsOS
    {
      get
      {
        return
          !(Environment.OSVersion.Platform == PlatformID.MacOSX ||
            Environment.OSVersion.Platform == PlatformID.Unix ||
            Environment.OSVersion.Platform == PlatformID.Xbox);
      }
    }

  }
}
