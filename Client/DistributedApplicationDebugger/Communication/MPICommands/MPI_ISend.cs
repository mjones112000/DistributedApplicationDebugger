using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_ISend : MPICommand, IMessageCommand
  {
    public const string CommandName = "MPI_ISEND";

    public MPI_ISend(int sourceId, List<BufferValue> buf, int count, string datatype, int dest, int tag, string comm, string request):base(sourceId)
    {
      Buf = buf;
      Count = count;
      DataType = datatype;
      Dest = dest;
      Tag = new MPI_TAG(tag);
      Comm = comm;
      Request = request;

    }

    public MPI_ISend(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {
      Count = Int32.Parse(parameterList[0]);
      DataType = parameterList[1];
      Dest = Int32.Parse(parameterList[2]);
      Tag = new MPI_TAG(Int32.Parse(parameterList[3]));
      Comm = parameterList[4];
      Request = parameterList[5];

      if (!String.IsNullOrEmpty(parameterList[6]))
          ExpectedCount = Int32.Parse(parameterList[6]);

      if (!String.IsNullOrEmpty(parameterList[7]))
          ExpectedDataType = parameterList[7];

      if (!String.IsNullOrEmpty(parameterList[8]))
          ExpectedDest = Int32.Parse(parameterList[8]);

      if (!String.IsNullOrEmpty(parameterList[9]))
          ExpectedTag = new MPI_TAG(Int32.Parse(parameterList[9]));
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

        if (ExpectedDest.HasValue)
            descriptionBuf.AppendLine(String.Format("Dest: {0} [{1}]", Dest, ExpectedDest.Value));
        else
            descriptionBuf.AppendLine(String.Format("Dest: {0}", Dest));

        if (ExpectedTag != null)
            descriptionBuf.AppendLine(String.Format("Tag: {0} [{1}]", Tag, ExpectedTag));
        else
            descriptionBuf.AppendLine(String.Format("Tag: {0}", Tag));

        descriptionBuf.AppendLine(String.Format("Comm: {0}", Comm));
        descriptionBuf.AppendLine(String.Format("Request: {0}", Request));

        if (ExpectedReturnValue != null)
            descriptionBuf.Append(String.Format("Return Value: {0} [{1}]", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty, ExpectedReturnValue));
        else
            descriptionBuf.Append(String.Format("Return Value: {0}", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty));

        return descriptionBuf.ToString();
      }
    }

    public override string ToString()
    {
      return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", CommandName, Buf, Count, DataType, Dest, Tag, Comm, Request);
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
        Protocal.PARTITION, Buf, Count, DataType, Dest, Tag, Comm, Request));
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
            return !ExpectedCount.HasValue && !ExpectedDest.HasValue && !ExpectedReturnValue.HasValue &&
                    ExpectedDataType == null && ExpectedTag == null && InvalidBuf == null;
        }
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


    public int? Dest
    {
      get;
      private set;
    }

    public int? ExpectedDest
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

    public string Request
    {
      get;
      set;
    }

    public override IMessageCommand MatchingMessageCommand
    {
      get;
      set;
    }

    public int? MessageId
    {
      get;
      set;
    }

    public MPI_SOURCE Src
    {
      get{return null;}
    }

    public int MessageLinkId
    {
      get { return Dest.Value; }
    }

    public MessageDisplayState DisplayState
    {
      get;
      set;
    }

    public bool IsMatch(IMessageCommand command)
    {
      return command.DataType.Equals(this.DataType, StringComparison.CurrentCultureIgnoreCase) &&
             command.Src != null &&
             command.Src.ActualValue == this.NodeId &&
             command.NodeId == this.Dest &&
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

    protected override void Complete(string[] completeValues)
    {
        this.InvalidBuf = ParseBufferValues(completeValues);
    }
  }
}
