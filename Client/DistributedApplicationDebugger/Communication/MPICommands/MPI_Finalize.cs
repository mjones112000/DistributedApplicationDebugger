using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Finalize : MPICommand   
  {
    public const string CommandName = "MPI_FINALIZE";

    public MPI_Finalize(int sourceId):base(sourceId)
    {
    }

    public MPI_Finalize(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {

    }

    protected override string Description
    {
        get
        {
            StringBuilder descriptionBuf = new StringBuilder();

            if (ExpectedReturnValue != null)
                descriptionBuf.Append(String.Format("Return Value: {0} [{1}]", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty, ExpectedReturnValue));
            else
                descriptionBuf.Append(String.Format("Return Value: {0}", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty));

            return descriptionBuf.ToString();
        }
    }

    public override string ToString()
    {
      return CommandName;
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(string.Empty);
        
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
