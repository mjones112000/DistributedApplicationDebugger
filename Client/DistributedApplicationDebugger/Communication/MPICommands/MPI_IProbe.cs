using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_IProbe : MPICommand
  {
    public const string CommandName = "MPI_IPROBE";

    public MPI_IProbe(int sourceId, int src, int tag, string comm, int flag, MPI_Status status):base(sourceId)
    {
      Src = new MPI_SOURCE(src);
      Tag = new MPI_TAG(tag);
      Comm = comm;
      Flag = flag;
      Status = status;
    }

    public MPI_IProbe(int sourceId, int commandId, int lineNum, string[] parameterList)
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
        descriptionBuf.AppendLine(String.Format("Flag: {0}", Flag));

        descriptionBuf.AppendLine(String.Format("Status:"));
          
        descriptionBuf.AppendLine(String.Format("  Source: {0}", Status == null? string.Empty: Status.Source.ToString()));
        descriptionBuf.AppendLine(String.Format("  Tag: {0}", Status == null? string.Empty: Status.Tag.ToString()));
        descriptionBuf.AppendLine(String.Format("  Error: {0}", Status == null? string.Empty: Status.Error));

        if (ExpectedReturnValue != null)
            descriptionBuf.Append(String.Format("Return Value: {0} [{1}]", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty, ExpectedReturnValue));
        else
            descriptionBuf.Append(String.Format("Return Value: {0}", ReturnValue.HasValue ? ReturnValue.Value.ToString() : string.Empty));

        return descriptionBuf.ToString();
      }
    }

    public override string ToString()
    {
      return String.Format("{0} {1} {2} {3} {4} {5}", CommandName, Src, Tag, Comm, Flag, Status.ToString());
    }

    protected override byte[] SerializeCommand()
    {
      return Encoding.ASCII.GetBytes(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
        Protocal.PARTITION, Src, Tag, Comm, Flag, Status.ToString()));
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
            return !ExpectedReturnValue.HasValue && ExpectedSrc == null && ExpectedTag == null;
        }
    }

    protected override void Complete(string[] completeValues)
    {
      this.Flag = Int32.Parse(completeValues[0]);
      if (Flag == 1)
      {
          this.Status = new MPI_Status(completeValues[1]);
          this.Src.ActualValue = Status.Source.ActualValue;
          this.Tag.ActualValue = Status.Tag.ActualValue;
      }
    }

    private MPI_SOURCE Src
    {
      get;
      set;
    }

    private MPI_SOURCE ExpectedSrc
    {
        get;
        set;
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

    private int? Flag
    {
      get;
      set;
    }

    private MPI_Status Status
    {
      get;
      set;
    }
  }
}
