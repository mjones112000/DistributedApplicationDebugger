using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedApplicationDebugger;
using SSHLib;

namespace DistributedApplicationDebugger
{
  public class SSHCommpleteEventArgs : EventArgs
  {
    public SSHCommpleteEventArgs(SSHManager sshManager, SSHConfiguration previousConnection, SSHConfiguration currentConnection)
    {
      SSHManager = sshManager;
      PreviousConnection = previousConnection;
      CurrentConnection = currentConnection;
    }

    public SSHManager SSHManager
    {
      get;
      private set;
    }

    public SSHConfiguration PreviousConnection
    {
      get;
      private set;
    }

    public SSHConfiguration CurrentConnection
    {
      get;
      private set;
    }
  }
}
