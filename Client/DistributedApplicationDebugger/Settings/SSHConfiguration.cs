using System;
using System.Xml.Serialization;
using System.ComponentModel;

namespace DistributedApplicationDebugger.Settings
{
  [Serializable]
  public class SSHConfiguration
  {
    private string _remoteComputer = string.Empty;
    private string _userName = string.Empty;
    private string _password = string.Empty;
    private string _transferDirectory = string.Empty;
    private string _feedbackPort = string.Empty;
    public event PropertyChangedEventHandler PropertyChanged;

    public SSHConfiguration()
    {
    }

    public string RemoteComputer
    {
      get { return _remoteComputer; }
      set
      {
        _remoteComputer = value;
        NotifyPropertyChanged("RemoteComputer");
      }
    }

    public string UserName
    {
      get { return _userName; }
      set
      {
        _userName = value;
        NotifyPropertyChanged("UserName");
      }
    }

    //[XmlIgnore]
    public string Password
    {
      get { return _password; }
      set
      {
        _password = value;
        NotifyPropertyChanged("Password");
      }
    }

    public string TransferDirectory
    {
      get { return _transferDirectory; }
      set
      {
        _transferDirectory = value;
        NotifyPropertyChanged("TransferDirectory");
      }
    }

    public string FeedbackPort
    {
      get { return _feedbackPort; }
      set
      {
        _feedbackPort = value;
        NotifyPropertyChanged("FeedbackPort");
      }
    }

    public string SSHDisplayMember
    {
      get { return "RemoteComputer"; }
    }

    private void NotifyPropertyChanged(String info)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(info));
      }
    }

    public void RefreshProperties()
    {

    }
  }
}
