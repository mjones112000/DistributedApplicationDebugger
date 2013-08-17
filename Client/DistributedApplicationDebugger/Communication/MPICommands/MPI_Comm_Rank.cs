using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Comm_Rank: MPICommand
  {
    public const string CommandName = "MPI_RANK";

    public MPI_Comm_Rank(int sourceId, string comm, int rank):base(sourceId)
    {
      Comm = comm;
      Rank = rank;
    }

    public MPI_Comm_Rank(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {
      Comm = parameterList[0];
    }

    protected override string Description
    {
        get
        {
            StringBuilder descriptionBuf = new StringBuilder();
            descriptionBuf.AppendLine(String.Format("Rank: {0}", Rank));
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
      return String.Format("{0} {1} {2}", CommandName, Comm, Rank);
    }

    private string Comm
    {
      get;
      set;
    }

    private int? Rank
    {
      get;
      set;
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}", Protocal.PARTITION,Comm, Rank));
    }

    public override string Name
    {
      get
      {
        return CommandName;
      }
    }

    protected override void Complete(string[] completeValues)
    {
      this.Rank = Int32.Parse(completeValues[0]);
    }
  }
}
