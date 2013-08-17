using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DistributedApplicationDebugger.Presenters;
using DistributedApplicationDebugger.Settings;
using SSHLib;

namespace DistributedApplicationDebugger.Views
{
  public partial class MainFormView : Form
  {
    MainFormPresenter presenter = null;
    private bool _manuallyResizing = false;

    public MainFormView()
    {
      InitializeComponent();
    }

    private void MainFormView_Load(object sender, EventArgs e)
    {
      Func<ConnectionStatus, bool> connectionStatusFunc = (cs) =>
        presenter.Settings.SessionStatus == cs;

      Func<ConnectionStatus[], bool> connectionStatusesFunc = (cs) =>
        cs.Contains(presenter.Settings.SessionStatus);

      Func<SettingsManager, bool> connectInfoValidFunc = (settings) =>
        settings.SessionStatus == ConnectionStatus.Idle &&
        settings.SSHConfigurations.Count > 0 && !settings.SSHConfigurations.Any(x => !x.IsValid);

      presenter = new Presenters.MainFormPresenter(this, playButtonToolStrip);

      playButton.DropDownDirection = ToolStripDropDownDirection.BelowRight;

      displayGridButton.Checked = presenter.Settings.DisplayBufferGrid;

      this.lstTransactions.SelectedIndexChanged += (s, ev) => presenter.Settings.SelectedConfiguration = lstTransactions.SelectedValue as SSHConfiguration;
      this.FormClosed += (s, ev) => presenter.MainFormClosed();
      this.upButton.Click += (s, ev) => presenter.MoveSSHConfigurationUp();
      this.downButton.Click += (s, ev) => presenter.MoveSSHConfigurationDown();
      this.connectionButton.Click += (s, ev) => 
      {
        displayGridButton.Enabled = false;
        displayGridButton.Checked = false;
        nodePanelHost1.Reset();
        presenter.Connect();
      };

      this.cancelButton.Click += (s, ev) =>
        {
          presenter.Settings.ReplaySession = null;
          presenter.Disconnect();
          nodePanelHost1.SessionMode = SessionMode.Offline;
        };

      this.playButton.Click += (s, ev) => 
        {
          if(String.IsNullOrEmpty(presenter.Settings.ExecutableName))
            MessageBox.Show("Executable Location must be provided", Application.ProductName);

          if (presenter.Settings.ReplaySession == null)
          {
            displayGridButton.Enabled = false;
            displayGridButton.Checked = false;
            nodePanelHost1.Reset();
            nodePanelHost1.SessionMode = SessionMode.Play;
            presenter.PlayButtonClicked(playButton, nodePanelHost1.GDBNodes);
          }
          else
          {
            if (nodePanelHost1.PlaybackNodes.Count() == 0)
            {
              MessageBox.Show("At least one node must be designated as 'Playback' in replay mode.", Application.ProductName);
            }
            else
            {
              displayGridButton.Enabled = true;
              nodePanelHost1.Reset();
              nodePanelHost1.SessionMode = SessionMode.Replay;
              presenter.ReplayButtonClicked(nodePanelHost1.PlaybackNodes, nodePanelHost1.GDBNodes);
            
            }
          }
        };

      this.recordButton.Click += (s, ev) =>
      {
        displayGridButton.Enabled = true;
        nodePanelHost1.Reset();
        nodePanelHost1.SessionMode = SessionMode.Record;
        presenter.RecordButtonClicked(nodePanelHost1.GDBNodes);
      };

      this.connectinfoCollapseButton.Click += (s, ev) =>
        {
          presenter.ConnectInfoButtonClick();
          nodePanelHost1.ResetHorizonatalScroll();
        };

      this.connectinfoDisplayButton.Click += (s, ev) =>
        {
          presenter.ConnectInfoButtonClick();
          nodePanelHost1.ResetHorizonatalScroll();
        };

      this.addButton.Click += (s, ev) => presenter.AddSSHConfiguration();
      this.removeButton.Click += (s, ev) => presenter.RemoveSSHConfiguration();
      this.cancelReplayButton.Click += (s, ev) => { presenter.Settings.ReplaySession = null;};
      this.displayGridButton.CheckedChanged += (s, ev) => { presenter.Settings.DisplayBufferGrid = displayGridButton.Checked; };

      this.settingsButton.Click += (s, ev) => 
      {
        SettingsView settingsWindow = new SettingsView(presenter.Settings.SOHReplacement, 
                          presenter.Settings.BARReplacement, presenter.Settings.EOTReplacement);
        if (settingsWindow.ShowDialog() == DialogResult.OK)
        {
          presenter.Settings.SOHReplacement = settingsWindow.SOHReplacement;
          presenter.Settings.BARReplacement = settingsWindow.BARReplacement;
          presenter.Settings.EOTReplacement = settingsWindow.EOTReplacement;
        }
      };

      presenter.IncomingData += nodePanelHost1.IncomingData;
      nodePanelHost1.ExecutableFolderReceived += (s,ve) => presenter.Settings.ExecutableFolder = ve.Value;

      nodePanelHost1.FileNotFound += presenter.FileNotFoundDetected;
      nodePanelHost1.HistoryListReceived += presenter.SessionHistoryReceived;
      nodePanelHost1.FinalizedProcessed += presenter.FinalizedCountUpdate;
      nodePanelHost1.MessageBufferRequested += (s, ve) => presenter.MessageBufferRequested(ve.Value);
      nodePanelHost1.RecordSessionNameReceived += (s, ve) => presenter.Settings.ReplaySessionName = ve.Value;
      nodePanelHost1.RecordSessionInfoReceived += (s, ve) => presenter.RecordSessionReceived(ve.Value);
      nodePanelHost1.GdbRequestReceived += (s, ve) => presenter.GDBRequestIssued(ve.NodeId, ve.Message);

      AddBinding(hostFileTextBox, "Text", "HostFile");

      AddBindings(new Control[] { hostFileLabel, hostFileTextBox, executableLabel, executableTextBox, 
        sessionNameLabel, sessionNameTextBox, paramsLabel, paramsTextBox, numberOfNodesLabel, nodesCounter}, "Enabled",
        () => presenter.Settings.ReplaySession == null && 
              !connectionStatusFunc(ConnectionStatus.Running), "ReplaySession", "SessionStatus");

      AddBinding(executableTextBox, "Text", "ExecutableName");
      AddBinding(sessionNameTextBox, "Text", "SessionName");
      AddBinding(paramsTextBox, "Text", "Params");

      AddBinding(lstTransactions, "DisplayMember", "SelectedConfiguration.SSHDisplayMember");
      AddBinding(lstTransactions, "DataSource", "SSHConfigurations");
      AddBinding(lstTransactions, "SelectedItem", "SelectedConfiguration");
      AddBinding(nodesCounter, "Value", "NodeCount");
      AddBinding(remoteComputerTextBox, "Text", "SelectedConfiguration.RemoteComputer");
      AddBinding(userNameTextBox, "Text", "SelectedConfiguration.UserName");
      AddBinding(passwordTextBox, "Text", "SelectedConfiguration.Password");
      AddBinding(transferDirectoryTextBox, "Text", "SelectedConfiguration.TransferDirectory");
      AddBinding(connectionPortTextBox, "Text", "SelectedConfiguration.ConnectionPort");
      AddBinding(connectionLogTextBox, "Text", "ConnectionLog");
      AddBinding(rawDataTextBox, "Text", "RawStatusLog");
      AddBinding(statusPictureBox, "Image", () => presenter.Settings.StatusImage, "SessionStatus");
      AddBinding(mainSplitter, "Panel2Collapsed", () => !presenter.Settings.IsConnectionInfoVisible, "IsConnectionInfoVisible");
      AddBinding(mainViewFooter, "Visible", () => !presenter.Settings.IsConnectionInfoVisible, "IsConnectionInfoVisible");
      
      AddBinding(remoteConnectionTabs, "SelectedIndex", "SelectedSSHTabIndex");
      AddBinding(playButtonToolStrip, "DropDownList", "ReplayButtons");
      AddBinding(nodePanelHost1, "NodeCount", "NodeCount");
      AddBinding(nodePanelHost1, "ReplayMode", () => presenter.Settings.ReplaySession != null, "ReplaySession");
      AddBinding(nodePanelHost1, "SessionRunning", () => connectionStatusFunc(ConnectionStatus.Running), "SessionStatus");
      AddBinding(nodePanelHost1, "SessionIdle", () => connectionStatusFunc(ConnectionStatus.Idle), "SessionStatus");
      AddBinding(nodePanelHost1, "SOHReplacement", "SOHReplacement");
      AddBinding(nodePanelHost1, "BARReplacement", "BARReplacement");
      AddBinding(nodePanelHost1, "EOTReplacement", "EOTReplacement");
      AddBinding(nodePanelHost1, "DisplayBufferGrid", "DisplayBufferGrid");

      AddBinding(connectionButtonToolStrip, "Enabled", () => connectInfoValidFunc(presenter.Settings),
        "SSHConfigurations", "SessionStatus");

      AddBinding(cancelButtonToolStrip,
        "Enabled", () => !connectionStatusFunc(ConnectionStatus.Idle), "SessionStatus");

      AddBinding(playButtonToolStrip, "Enabled", () => 
        connectionStatusesFunc(new ConnectionStatus[]{ConnectionStatus.Connected, ConnectionStatus.Complete}), "SessionStatus");

      AddBinding(executablePrefixLabel, "Text", "ExecutableFolder");
      AddBinding(executableSplitContainer, "SplitterDistance", () => executablePrefixLabel.Width, "ExecutableFolder");

      AddBinding(recordButtonToolStrip,
        "Enabled", () => connectionStatusesFunc(new ConnectionStatus[] { ConnectionStatus.Connected, ConnectionStatus.Complete }) && 
          !String.IsNullOrEmpty(presenter.Settings.ExecutableName) && 
          !String.IsNullOrEmpty(presenter.Settings.SessionName) && presenter.Settings.ReplaySession == null, 
          "SessionStatus", "ExecutableName", "SessionName", "ReplaySession");

      AddBinding(splitTransList.Panel1,
        "Enabled", () => presenter.Settings.SessionStatus == ConnectionStatus.Idle, "SessionStatus");

      AddBindings(new Control[] { removeButtonToolStrip, downButtonToolStrip, upButtonToolStrip },
        "Enabled", () => presenter.Settings.SSHConfigurations.Count > 0, "SSHConfigurations");

      AddBinding(remoteConnectionsSplitter.Panel2, "Enabled",
        () => presenter.Settings.SessionStatus == ConnectionStatus.Idle && presenter.Settings.SSHConfigurations.Count > 0, "SessionStatus", "SSHConfigurations");

      AddBindings(new Control[] { replayLabel, cancelReplayButtonToolStrip },
        "Enabled", () => presenter.Settings.ReplaySession != null && !connectionStatusFunc(ConnectionStatus.Running),
        "ReplaySession", "SessionStatus");

      AddBindings(new Control[]{replayModeLabel, replaySessionLabel,
      replayLabel, cancelReplayButtonToolStrip}, "Visible", () => presenter.Settings.ReplaySession != null, "ReplaySession");

      AddBinding(replaySessionLabel, "Text", "ReplaySessionName");

      nodePanelHost1.ResetHorizonatalScroll();
    }

    public RedrawToolStripSplitButton PlayButton
    {
      get { return playButton; }
    }

    private void AddBinding(Control control, string propertyName, string dataMember)
    {
      presenter.Settings.AddBinding(control, propertyName, dataMember);
    }

    private void AddBinding<T>(Control control, string propertyName, Func<T> converter, params string[] dataMembers)
    {
      presenter.Settings.AddBinding(control, propertyName, converter, dataMembers);
    }

    private void AddBindings(IEnumerable<Control> controls, string propertyName, string dataMember)
    {
      foreach (Control control in controls)
      {
        AddBinding(control, propertyName, dataMember);
      }
    }

    private void AddBindings<T>(IEnumerable<Control> controls, string propertyName, Func<T> converter, params string[] dataMembers)
    {
      foreach (Control control in controls)
      {
        AddBinding(control, propertyName, converter, dataMembers);
      }
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams cp = base.CreateParams;
        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        return cp;
      }
    }

    StreamReader reader = null;
    string[] fileContents = null;
    private void readFromFileButton_Click(object sender, EventArgs e)
    {
      int commonPathEnd = Application.StartupPath.IndexOf(Application.ProductName);
      string filePath = string.Empty;
      if (commonPathEnd > -1)
        filePath = String.Format("{0}TestDataGenerator\\bin\\Debug\\TestData.txt", Application.StartupPath.Substring(0, commonPathEnd));
      else
        filePath = String.Format("{0}\\TestData.txt", Application.StartupPath);

      if (nodePanelHost1.SessionIdle)
      {
        nodePanelHost1.SessionIdle = false;
      }

      if (fileContents == null)
      {
        if (File.Exists(filePath))
        {
          using (reader = new StreamReader(filePath))
          {
            fileContents = reader.ReadToEnd().Split(new string[]{"\r\n"}, StringSplitOptions.None);
            reader.Close();
          }
        }
        else
        {
          MessageBox.Show(String.Format("File {0} not found.", filePath));
          return;
        }

      }

      
      if (!fileContents.Any())
      {
        MessageBox.Show("EOF of file");
        fileContents = null;
      }
      else
      {
        string readLine = fileContents[0];
        nodePanelHost1.IncomingData(this, new ValueEventArgs<string>(readLine.Replace("\n", Environment.NewLine)));
        fileContents = fileContents.ToList().Skip(1).ToArray();
        //MessageBox.Show(readLine);
      }
    }

    private void ReadInAllCommands()
    {
      int commonPathEnd = Application.StartupPath.IndexOf(Application.ProductName);
      string filePath = string.Empty;
      
      if (commonPathEnd > -1)
        filePath = String.Format("{0}TestDataGenerator\\bin\\Debug\\TestData.txt", Application.StartupPath.Substring(0, commonPathEnd));
      else
        filePath = String.Format("{0}\\TestData.txt", Application.StartupPath);

      if (nodePanelHost1.SessionIdle)
      {
        nodePanelHost1.SessionIdle = false;
      }

      if (File.Exists(filePath))
      {
        using (StreamReader myReader = new StreamReader(filePath))
        {
          foreach (string readLine in myReader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None))
          {
            nodePanelHost1.IncomingData(this, new ValueEventArgs<string>(readLine.Replace("\n", Environment.NewLine)));
          }
          myReader.Close();
        }
      }
      else
      {
        MessageBox.Show(String.Format("File {0} not found.", filePath));
        return;
      }
    }

    private void MainFormView_Shown(object sender, EventArgs e)
    {
      nodePanelHost1.ResetHorizonatalScroll();
    }

    private void MainFormView_Resize(object sender, EventArgs e)
    {
        nodePanelHost1.ResetHorizonatalScroll();
    }
  }
}