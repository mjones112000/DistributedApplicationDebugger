
using System;
namespace DistributedApplicationDebugger
{
  public class GdbRequestEventArgs : EventArgs
  {
    public GdbRequestEventArgs(int nodeId, string message)
    {
      NodeId = nodeId;
      Message = message;
    }

    public int NodeId
    {
      get;
      private set;
    }

    public string Message
    {
      get;
      private set;
    }
  }
}