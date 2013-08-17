using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Recv : MPICommand, IMessageCommand
  {
    public const string CommandName = "MPI_RECV";

    public MPI_Recv(int sourceId, List<BufferValue> buf, int count, string datatype, int src, int tag, string comm, MPI_Status status):base(sourceId)
    {
      Buf = buf;
      Count = count;
      DataType = datatype;
      Src = new MPI_SOURCE(src);
      Tag = new MPI_TAG(tag);
      Comm = comm;
      Status = status;
      Src.ActualValue = Status.Source.ActualValue;
      Tag.ActualValue = Status.Tag.ActualValue;
    }

    public MPI_Recv(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {
      Count = Int32.Parse(parameterList[0]);
      DataType = parameterList[1];
      Src = new MPI_SOURCE(Int32.Parse(parameterList[2]));
      Tag = new MPI_TAG(Int32.Parse(parameterList[3]));
      Comm = parameterList[4];
      if (!String.IsNullOrEmpty(parameterList[5]))
          ExpectedCount = Int32.Parse(parameterList[5]);

      if (!String.IsNullOrEmpty(parameterList[6]))
          ExpectedDataType = parameterList[6];

      if (!String.IsNullOrEmpty(parameterList[7]))
          ExpectedSrc = new MPI_SOURCE(Int32.Parse(parameterList[7]));

      if (!String.IsNullOrEmpty(parameterList[8]))
          ExpectedTag = new MPI_TAG(Int32.Parse(parameterList[8]));
    }

    protected override string Description
    {
        get
        {
            StringBuilder bufVal = new StringBuilder();
            if (Buf != null && Buf.Count > 0)
            {
                int counter = 0;
                bufVal.Append(Buf[0].Value);
                foreach (BufferValue s in Buf.Skip(1))
                {
                    bufVal.Append(String.Format(", {0}", s.Value));
                    counter++;
                    if (counter > 5)
                    {
                        bufVal.Append("...");
                        break;
                    }
                }
            }

            StringBuilder invalidBufVal = new StringBuilder();
            if (InvalidBuf != null && InvalidBuf.Count > 0)
            {
                int counter = 0;
                invalidBufVal.Append(InvalidBuf[0].Value);
                foreach (BufferValue s in InvalidBuf.Skip(1))
                {
                    invalidBufVal.Append(String.Format(", {0}", s.Value));
                    counter++;
                    if (counter > 5)
                    {
                        invalidBufVal.Append("...");
                        break;
                    }
                }
            }

            StringBuilder descriptionBuf = new StringBuilder();

            if (InvalidBuf != null)
                descriptionBuf.AppendLine(String.Format("Buf: {0} [{1}]", bufVal.ToString(), invalidBufVal.ToString()));
            else
                descriptionBuf.AppendLine(String.Format("Buf: {0}", bufVal.ToString()));

            if (ExpectedCount.HasValue)
                descriptionBuf.AppendLine(String.Format("Count: {0} [{1}]", Count, ExpectedCount.Value));
            else
                descriptionBuf.AppendLine(String.Format("Count: {0}", Count));

            if (ExpectedDataType != null)
                descriptionBuf.AppendLine(String.Format("Datatype: {0} [{1}]", DataType, ExpectedDataType));
            else
                descriptionBuf.AppendLine(String.Format("Datatype: {0}", DataType));

            if (ExpectedSrc != null)
                descriptionBuf.AppendLine(String.Format("Src: {0} [{1}]", Src, ExpectedSrc.Value));
            else
                descriptionBuf.AppendLine(String.Format("Src: {0}", Src));

            if (ExpectedTag != null)
                descriptionBuf.AppendLine(String.Format("Tag: {0} [{1}]", Tag, ExpectedTag));
            else
                descriptionBuf.AppendLine(String.Format("Tag: {0}", Tag));

            descriptionBuf.AppendLine(String.Format("Comm: {0}", Comm));

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
      return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} ", CommandName, Buf, Count, DataType, Src, Tag, Comm, Status.ToString());
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
        Protocal.PARTITION, Buf, Count, DataType, Src, Tag.ToString(), Comm, Status.ToString()));
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
            return !ExpectedReturnValue.HasValue && InvalidBuf == null && !ExpectedCount.HasValue &&
                    ExpectedDataType == null && ExpectedSrc == null && ExpectedTag == null && ExpectedStatus == null;
        }
    }

    protected override void Complete(string[] completeValues)
    {
      this.Status = new MPI_Status(completeValues[0]);
      this.Src.ActualValue = Status.Source.ActualValue;
      this.Tag.ActualValue = Status.Tag.ActualValue;

      if (!String.IsNullOrEmpty(completeValues[1]))
          ExpectedStatus = new MPI_Status(completeValues[1]);

      this.InvalidBuf = ParseBufferValues(completeValues.Skip(2).ToArray());
    }

    public List<BufferValue> Buf
    {
      get;
      set;
    }

    public List<BufferValue> InvalidBuf
    {
        get;
        set;
    }

    public int Count
    {
      get;
      private set;
    }

    public int? ExpectedCount
    {
        get;
        private set;
    }

    public string DataType
    {
      get;
      private set;
    }

    public string ExpectedDataType
    {
        get;
        private set;
    }

    public MPI_SOURCE Src
    {
      get;
      private set;
    }

    public MPI_SOURCE ExpectedSrc
    {
        get;
        private set;
    }

    public MPI_TAG Tag
    {
      get;
      private set;
    }

    public MPI_TAG ExpectedTag
    {
        get;
        private set;
    }

    private string Comm
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

    public int? MessageId
    {
      get;
      set;
    }

    public override IMessageCommand MatchingMessageCommand
    {
      get;
      set;
    }

    public int? Dest
    {
      get { return null; }
    }

    public int MessageLinkId
    {
      get
      {
        return Src.ActualValue;
      }
    }

    public MessageDisplayState DisplayState
    {
      get;
      set;
    }

    public bool IsMatch(IMessageCommand command)
    {
      return command.DataType.Equals(this.DataType, StringComparison.CurrentCultureIgnoreCase) &&
             command.Dest != null && 
             command.Dest == this.NodeId &&
             command.NodeId == this.Src.ActualValue &&
             command.Tag.Equals(this.Tag);
    }

    public DataRow MessageDataRow
    {
      get;
      set;
    }

    public int? BufferCommandId
    {
      get { return CommandId; }
    }
  }
}
