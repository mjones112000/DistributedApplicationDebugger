using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication
{
  public class MessageReadEventArgs :EventArgs
  {
    public MessageReadEventArgs(string message)
    {
      Message = message;
    }

    public string Message
    {
      get;
      private set;
    }
  }
}
