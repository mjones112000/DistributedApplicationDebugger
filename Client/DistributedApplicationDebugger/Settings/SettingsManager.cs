using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;
using System.Windows.Forms;
using SSHLib;
using DistributedApplicationDebugger.Properties;
using System.Runtime.Serialization;

namespace DistributedApplicationDebugger.Settings
{
  public enum ConnectionStatus
  {
    Idle,
    Connecting,
    Connected,
    Running,
    Complete,
    Error
  }

  [Serializable]
  public class SettingsManager
  {

    public event PropertyChangedEventHandler PropertyChanged;

    private int _nodeCount = 0;
    private int _selectedSSHTabIndex = 0;
    private bool _displayBufferGrid = false;

    private string _hostFile = string.Empty;
    private string _executableFolder = string.Empty;
    private string _executableName = string.Empty;
    private string _sessionName = string.Empty;
    private string _params = string.Empty;

    private string _connectionLog = string.Empty;
    private string _rawStatusLog = string.Empty;
    private string _replaySessionName = string.Empty;

    private string _sohReplacement = "*SOH*";
    private string _barReplacement = "*BAR*";
    private string _eotReplacement = "*EOT*";

    private bool _isConnectionInfoVisible = true;

    private BindingList<SSHConfiguration> _sshConfigurations = new BindingList<SSHConfiguration>();
    private SSHConfiguration _selectedConfiguration = null;

    private Dictionary<string, List<Binding>> _bindingList = new Dictionary<string, List<Binding>>();

    private ConnectionStatus _sessionStatus = ConnectionStatus.Idle;
    private BindingList<ToolStripMenuItem> _replayButtons = new BindingList<ToolStripMenuItem>();
    private SessionInfo _replaySession = null;

    public SettingsManager()
    {
      _sshConfigurations.ListChanged += ((sender, e) =>
      {
        if (e.ListChangedType == ListChangedType.ItemAdded)
        {
          SSHConfiguration sshConfiguration = _sshConfigurations[e.NewIndex] as SSHConfiguration;
          sshConfiguration.PropertyChanged += ((sender2, e2) =>
          {
            SSHConfigurations.ResetItem(_sshConfigurations.IndexOf(sshConfiguration));
            _bindingList["SSHConfigurations"][0].Control.Refresh();
          });
        }
        NotifyPropertyChanged("SSHConfigurations");
      });

      _replayButtons.ListChanged += (sender, e) => NotifyPropertyChanged("ReplayButtons");
    }

    public int NodeCount
    {
      get { return _nodeCount; }
      set
      {
        _nodeCount = value;
        NotifyPropertyChanged("NodeCount");
      }
    }

    public string HostFile
    {
      get { return _hostFile; }
      set
      {
        _hostFile = value;
        NotifyPropertyChanged("HostFile");
      }
    }

    [XmlIgnore]
    public string ExecutableFolder
    {
      get { return _executableFolder; }
      set
      {
        _executableFolder = value;
        NotifyPropertyChanged("ExecutableFolder");
      }
    }

    public string ExecutableName
    {
      get { return _executableName; }
      set
      {
        _executableName = value;
        NotifyPropertyChanged("ExecutableName");
      }
    }

    public string SessionName
    {
      get { return _sessionName; }
      set
      {
        _sessionName = value;
        NotifyPropertyChanged("SessionName");
      }
    }

    public string Params
    {
      get { return _params; }
      set
      {
        _params = value;
        NotifyPropertyChanged("Params");
      }
    }

    public bool IsConnectionInfoVisible
    {
      get { return _isConnectionInfoVisible; }
      set
      {
        _isConnectionInfoVisible = value;

        NotifyPropertyChanged("IsConnectionInfoVisible");
      }
    }

    public BindingList<SSHConfiguration> SSHConfigurations
    {
      get { return _sshConfigurations; }
      set
      {
        _sshConfigurations = value;
        NotifyPropertyChanged("SSHConfigurations");
      }
    }

    public SSHConfiguration SelectedConfiguration
    {
      get
      {
        if (_selectedConfiguration == null)
        {
          return new SSHConfiguration();
        }
        else
        {
          return _selectedConfiguration;
        }
      }
      set
      {
        if (_selectedConfiguration != value && SSHConfigurations.Count > 0)
        {
          _selectedConfiguration = value;
          NotifyPropertyChanged("SelectedConfiguration");
        }
      }
    }

    public void SwapConfigurations(int itemOneIndex, int itemTwoIndex)
    {

      SSHConfiguration firstSSHConfiguration = SSHConfigurations[itemOneIndex];
      SSHConfigurations[itemOneIndex] = SSHConfigurations[itemTwoIndex];
      SSHConfigurations[itemTwoIndex] = firstSSHConfiguration;

      SSHConfigurations.ResetBindings();
    }

    public void AddBinding(Control control, string propertyName, string dataMember)
    {
      Binding binding = control.DataBindings.OfType<Binding>().FirstOrDefault(x => x.PropertyName == propertyName);

      if (binding == null)
      {
        binding = new Binding(propertyName, this, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
        control.DataBindings.Add(binding);
      }

      List<Binding> bindingList = null;
      if (_bindingList.Keys.Contains(dataMember))
      {
        bindingList = _bindingList[dataMember];
      }
      else
      {
        bindingList = new List<Binding>();
        _bindingList.Add(dataMember, bindingList);
      }

      bindingList.Add(binding);
      int lastDot = dataMember.LastIndexOf(".");
      if (lastDot > 0)
      {
        string subString = dataMember.Substring(0, lastDot);
        AddBinding(control, propertyName, dataMember.Substring(0, lastDot));
      }
    }

    public void AddBinding<T>(Control control, string propertyName, Func<T> converter, params string[] dataMembers)
    {
      Binding binding = control.DataBindings.OfType<Binding>().FirstOrDefault(x => x.PropertyName == propertyName);

      if (binding == null)
      {
        binding = new Binding(propertyName, new BindingConverter<T>(converter), "Value");
        control.DataBindings.Add(binding);
      }

      foreach (string dataMember in dataMembers)
      {
        List<Binding> bindingList = null;
        if (_bindingList.Keys.Contains(dataMember))
        {
          bindingList = _bindingList[dataMember];
        }
        else
        {
          bindingList = new List<Binding>();
          _bindingList.Add(dataMember, bindingList);
        }

        bindingList.Add(binding);
      }
    }


    public string SOHReplacement
    {
      get { return _sohReplacement; }
      set
      {
        _sohReplacement = value;
        NotifyPropertyChanged("SOHReplacement");
      }
    }


    public string BARReplacement
    {
      get { return _barReplacement; }
      set
      {
        _barReplacement = value;
        NotifyPropertyChanged("BARReplacement");
      }
    }


    public string EOTReplacement
    {
      get { return _eotReplacement; }
      set
      {
        _eotReplacement = value;
        NotifyPropertyChanged("EOTReplacement");
      }
    }

    [XmlIgnore]
    public bool DisplayBufferGrid
    {
      get { return _displayBufferGrid; }
      set
      {
        _displayBufferGrid = value;
        NotifyPropertyChanged("DisplayBufferGrid");
      }
    }

    [XmlIgnore]
    public Image StatusImage
    {
      get
      {
        switch (SessionStatus)
        {
          case ConnectionStatus.Idle:
            return Resources.Circle_Grey;

          case ConnectionStatus.Connecting:
            return Resources.Circle_Blue;

          case ConnectionStatus.Connected:
            return Resources.Circle_Orange;

          case ConnectionStatus.Running:
            return Resources.Circle_Yellow;

          case ConnectionStatus.Complete:
            return Resources.Circle_Green;

          case ConnectionStatus.Error:
            return Resources.Circle_Red;

          default:
            return Resources.Circle_Grey;

        }
      }
    }

    [XmlIgnore]
    public Image ConnectionInfoButtonImage
    {
      get
      {
        if (IsConnectionInfoVisible)
          return Resources.ArrowDown;
        else
          return Resources.ArrowUp;
      }
    }

    [XmlIgnore]
    public string ConnectionInfoToolTip
    {
      get 
      {
        if(IsConnectionInfoVisible)
          return "Hide Connection Information";
        else
          return "Display Connection Information";
      }
    }

    [XmlIgnore]
    public string ConnectionLog
    {
      get { return _connectionLog; }
      set
      {
        _connectionLog = value;
        NotifyPropertyChanged("ConnectionLog");
      }
    }

    [XmlIgnore]
    public string RawStatusLog
    {
      get { return _rawStatusLog; }
      set
      {
        _rawStatusLog = value;
        NotifyPropertyChanged("RawStatusLog");
      }
    }

    [XmlIgnore]
    public ConnectionStatus SessionStatus
    {
      get { return _sessionStatus; }
      set
      {
        _sessionStatus = value;
        NotifyPropertyChanged("SessionStatus");
      }
    }

    [XmlIgnore]
    public int SelectedSSHTabIndex
    {
      get { return _selectedSSHTabIndex; }
      set 
      {
        _selectedSSHTabIndex = value;
        NotifyPropertyChanged("SelectedSSHTabIndex");
      }
    }

    [XmlIgnore]
    public BindingList<ToolStripMenuItem> ReplayButtons
    {
      get { return _replayButtons; }
      set
      {
        _replayButtons = value;
        NotifyPropertyChanged("ReplayButtons");
      }
    }

    [XmlIgnore]
    public string ReplaySessionName
    {
      get { return _replaySessionName; }
      set
      {
        _replaySessionName = value;

        NotifyPropertyChanged("ReplaySessionName");
      }
    }

    [XmlIgnore]
    public SessionInfo ReplaySession
    {
      get { return _replaySession; }
      set
      {
        _replaySession = value;

        if (_replaySession != null)
        {
          HostFile = _replaySession.HostFile;
          ExecutableName = _replaySession.Location.Replace(ExecutableFolder, string.Empty);
          NodeCount = _replaySession.Nodes;
          Params = _replaySession.Parameters;
          SessionName = _replaySession.SessionName;
          ReplaySessionName = _replaySession.StartTime.ToString();
        }
        else
        {
          ReplaySessionName = string.Empty;
        }
  

        NotifyPropertyChanged("ReplaySession");
      }
    }

    private void NotifyPropertyChanged(string info)
    {
      var bindingListKeyPair = _bindingList.FirstOrDefault(x => x.Key == info);

      if (bindingListKeyPair.Value != null)
      {
        foreach (Binding binding in bindingListKeyPair.Value)
        {
          binding.ReadValue();
        }
      }

      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(info));
      }
    }
  }
}
