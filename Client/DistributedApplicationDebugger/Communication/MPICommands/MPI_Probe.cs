using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Probe : MPICommand
  {
    public const string CommandName = "MPI_PROBE";

    public MPI_Probe(int sourceId, int src, int tag, string comm, MPI_Status status):base(sourceId)
    {
      Src = new MPI_SOURCE(src);
      Tag = new MPI_TAG(tag);
      Comm = comm;
      Status = status;
    }

    public MPI_Probe(int sourceId, int commandId, int lineNum, string[] parameterList)
      : base(sourceId, commandId, lineNum)
    {
      Src = new MPI_SOURCE(Int32.Parse(parameterList[0]));
      Tag = new MPI_TAG(Int32.Parse(parameterList[1]));
      Comm = parameterList[2];

      if (!String.IsNullOrEmpty(parameterList[3]))
          ExpectedSrc = new MPI_SOURCE(Int32.Parse(parameterList[3]));

      if (!String.IsNullOrEmpty(parameterList[4]))
          ExpectedTag = new MPI_TAG(Int32.Parse(parameterList[4]));
    }

    protected override string Description
    {
      get
      {
          StringBuilder descriptionBuf = new StringBuilder();

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
      return String.Format("{0} {1} {2} {3} {4}", CommandName, Src, Tag, Comm, Status.ToString());
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}{0}{3}{0}{4}",
        Protocal.PARTITION, Src, Tag, Comm, Status.ToString()));
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
            return !ExpectedReturnValue.HasValue && ExpectedSrc == null && ExpectedStatus == null && ExpectedTag == null;
        }
    }

    protected override void Complete(string[] completeValues)
    {
      this.Status = new MPI_Status(completeValues[0]);
      this.Src.ActualValue = Status.Source.ActualValue;
      this.Tag.ActualValue = Status.Tag.ActualValue;

      if (!String.IsNullOrEmpty(completeValues[1]))
          ExpectedStatus = new MPI_Status(completeValues[1]);
    }

      

    private MPI_SOURCE Src
    {
      get;
      set;
    }

    public MPI_SOURCE ExpectedSrc
    {
        get;
        private set;
    }

    private MPI_TAG Tag
    {
      get;
      set;
    }

    private MPI_TAG ExpectedTag
    {
        get;
        set;
    }

    private string Comm
    {
      get;
      set;
    }

    private MPI_Status Status
    {
      get;
      set;
    }

    public MPI_Status ExpectedStatus
    {
        get;
        set;
    }
  }
}
