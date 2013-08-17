using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Wait : MPICommand
  {
    public const string CommandName = "MPI_WAIT";

    public MPI_Wait(int sourceId, string request, MPI_Status status):base(sourceId)
    {
      Request = request;
      Status = status;
    }

    public MPI_Wait(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {
      Request = parameterList[0];
    }

    protected override string Description
    {
        get
        {
            StringBuilder descriptionBuf = new StringBuilder();

            descriptionBuf.AppendLine(String.Format("Request: {0}", Request));

            MPI_Status.AppendStatusDescription(descriptionBuf, Status, ExpectedStatus);

            if (ExpectedReturnValue != null)
                descriptionBuf.Append(String.Format("Return Value: {0} [{1}]", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty, ExpectedReturnValue));
            else
                descriptionBuf.Append(String.Format("Return Value: {0}", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty));

            return descriptionBuf.ToString();
        }
    }

    public override string ToString()
    {
      return String.Format("{0} {1} {2}", CommandName, Request, Status.ToString());
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}",
        Protocal.PARTITION, Request, Status.ToString()));
    }

    public override string Name
    {
      get
      {
        return CommandName;
      }
    }

    public override bool IsValid
    {
        get
        {
            return !ExpectedReturnValue.HasValue && ExpectedStatus == null;
        }
    }

    protected override void Complete(string[] completeValues)
    {

      this.Status = new MPI_Status(completeValues[0]);

      if (!String.IsNullOrEmpty(completeValues[1]))
          ExpectedStatus = new MPI_Status(completeValues[1]);

      this.InvalidBuf = ParseBufferValues(completeValues.Skip(2).ToArray());
    }

    public string Request
    {
      get;
      set;
    }

    public MPI_Status Status
    {
      get;
      set;
    }

    public MPI_Status ExpectedStatus
    {
        get;
        set;
    }

    public List<BufferValue> InvalidBuf
    {
        get;
        set;
    }
  }
}
