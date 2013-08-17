using DistributedApplicationDebugger.Communication.MPICommands;
using System;

namespace DistributedApplicationDebugger
{
  public class BufferRequestEventArgs : EventArgs
  {
    public BufferRequestEventArgs(IMessageCommand message, string debugFileLocation)
    {
      Message = message;
      DebugFileLocation = debugFileLocation;
    }

    public IMessageCommand Message
    {
      get;
      private set;
    }

    public string DebugFileLocation
    {
      get;
      private set;
    }
  }
}
