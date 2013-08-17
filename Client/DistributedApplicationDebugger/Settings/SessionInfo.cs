using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Drawing;
using System.Windows.Forms;
using SSHLib;
using DistributedApplicationDebugger.Properties;

namespace DistributedApplicationDebugger.Settings
{
  [Serializable]
  public class SessionInfo
  {
    public SessionInfo()
    {
    }

    public string SessionName
    {
      get;
      set;
    }

    public DateTime StartTime
    {
      get;
      set;
    }

    public int Nodes
    {
      get;
      set;
    }

    public string HostFile
    {
      get;
      set;
    }

    public string Location
    {
      get;
      set;
    }

    public string Parameters
    {
      get;
      set;
    }
  }
}
