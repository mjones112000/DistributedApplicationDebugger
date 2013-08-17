using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DistributedApplicationDebugger.Communication;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Barrier : MPICommand
  {
    public const string CommandName = "MPI_BARRIER";

    public MPI_Barrier(int nodeId, string comm):base(nodeId)
    {
      Comm = comm;
    }

    public MPI_Barrier(int nodeId, int commandId, int lineNum, string[] parameterList)
      : base(nodeId, commandId, lineNum)
    {
      Comm = parameterList[0];
    }

    protected override string Description
    {
        get
        {
            StringBuilder descriptionBuf = new StringBuilder();
            descriptionBuf.AppendLine(String.Format("Comm: {0}", Comm));

            if (ExpectedReturnValue != null)
                descriptionBuf.Append(String.Format("Return Value: {0} [{1}]", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty, ExpectedReturnValue));
            else
                descriptionBuf.Append(String.Format("Return Value: {0}", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty));

            return descriptionBuf.ToString();
        }
    }

    public override string ToString()
    {
      return String.Format("{0} {1}", CommandName, Comm);
    }

    private string Comm
    {
      get;
      set;
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{0}",Comm));
    }

    public override string Name
    {
      get
      {
        return CommandName;
      }
    }
  }
}
