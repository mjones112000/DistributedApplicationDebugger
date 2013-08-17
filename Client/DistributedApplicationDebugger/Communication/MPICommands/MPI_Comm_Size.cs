using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Comm_Size : MPICommand
  {
    public const string CommandName = "MPI_SIZE";

    public MPI_Comm_Size(int sourceId, string comm, int size):base(sourceId)
    {
      Comm = comm;
      Size = size;
    }

    public MPI_Comm_Size(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {
      Comm = parameterList[0];      
    }

    protected override string Description
    {
      get
      {
        StringBuilder descriptionBuf = new StringBuilder();
        descriptionBuf.AppendLine(String.Format("Size: {0}", Size));
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
      return String.Format("{0} {1} {2}", CommandName, Comm, Size);
    }

    private string Comm
    {
      get;
      set;
    }

    private int? Size
    {
      get;
      set;
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}",
        Protocal.PARTITION, Comm, Size));
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
      this.Size = Int32.Parse(completeValues[0]);
    }
  }
}
