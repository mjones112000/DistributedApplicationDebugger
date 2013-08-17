using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Net;

namespace SSHLib
{
  [Serializable]
  public class SSHConfiguration
  {
    private string _remoteComputer = string.Empty;
    private string _userName = string.Empty;
    private string _password = string.Empty;
    private string _transferDirectory = string.Empty;
    private string _connectionPort = string.Empty;
    public event PropertyChangedEventHandler PropertyChanged;

    public SSHConfiguration()
    {
      RemoteComputer = "New Connection";
    }

    public SSHConfiguration(
      string remoteComputer, string userName, string password,
      string transferDirectory, string connectionPort)
    {
      RemoteComputer = remoteComputer;
      UserName = userName;
      Password = password;
      TransferDirectory = transferDirectory;
      ConnectionPort = connectionPort;
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

    public string RemoteComputerAddress
    {
      get
      {
        IPAddress[] addresses = Dns.GetHostAddresses(RemoteComputer);
        if (addresses.Length > 0)
        {
          return addresses[0].ToString();
        }
        else
        {
          return RemoteComputer;
        }
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

    [XmlIgnore]
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

    public string ConnectionPort
    {
      get { return _connectionPort; }
      set
      {
        _connectionPort = value;
        NotifyPropertyChanged("ConnectionPort");
      }
    }

    public string SSHDisplayMember
    {
      get { return "RemoteComputer"; }
    }

    [XmlIgnore]
    public bool IsValid
    {
      get
      {
        int connectionPort = 0;
        bool validPort = Int32.TryParse(ConnectionPort, out connectionPort);

        return !IsStringNullOrWhitespace(RemoteComputer) &&
               !IsStringNullOrWhitespace(UserName) &&
               !IsStringNullOrWhitespace(Password) &&
               validPort;
      }
    }

    private void NotifyPropertyChanged(String info)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(info));
      }
    }

    private bool IsStringNullOrWhitespace(string value)
    {
      if (value == null)
      {
        return true;
      }

      return value.Trim().Length == 0;
    }
  }
}