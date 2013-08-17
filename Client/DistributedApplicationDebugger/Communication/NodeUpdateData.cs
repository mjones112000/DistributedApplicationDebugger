using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedApplicationDebugger.Communication.MPICommands;

namespace DistributedApplicationDebugger.Communication
{
  public class NodeUpdateData
  {
    public NodeUpdateData(int id)
    {
      Id = id;
      NewMpiCommands = new List<MPICommand>();
      UpdatedMpiCommands = new List<MPICommand>();
      StdOutConsole = new StringBuilder();
      GdbConsole = new StringBuilder();
      BufferUpdates = new Dictionary<int, List<BufferValue>>();
    }

    public int Id
    {
      get;
      private set;
    }

    public List<MPICommand> NewMpiCommands
    {
      get;
      private set;
    }

    public List<MPICommand> UpdatedMpiCommands
    {
      get;
      private set;
    } 

    public StringBuilder StdOutConsole
    {
      get;
      private set;
    }

    public StringBuilder GdbConsole
    {
      get;
      private set;
    }

    public Dictionary<int, List<BufferValue>> BufferUpdates
    {
      get;
      private set;
    }
  }
}

