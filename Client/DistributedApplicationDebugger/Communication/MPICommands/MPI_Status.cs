using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public class MPI_Status
  {
    public MPI_Status(int source, int tag, string error)
    {
      Source = new MPI_SOURCE(source);
      Tag = new MPI_TAG(tag);
      Error = error;
    }

    public MPI_Status(string status)
    {
      string[] splitString = status.Split(',');
      //Source = Int32.Parse(splitString[0]);
      //Tag = Int32.Parse(splitString[1]);
      Source = new MPI_SOURCE(Int32.Parse(splitString[0]));
      Tag = new MPI_TAG(Int32.Parse(splitString[1]));
      Error = splitString[2];
    }

    public MPI_SOURCE Source
    {
      get;
      private set;
    }

    public MPI_TAG Tag
    {
      get;
      private set;
    }

    public string Error
    {
      get;
      private set;
    }

    public override string ToString()
    {
      return string.Format("{0},{1},{2}", Source, Tag, Error);
    }

    public static void AppendStatusDescription(StringBuilder descriptionBuf, MPI_Status actualStatus, MPI_Status expectedStatus)
    {
        descriptionBuf.AppendLine(String.Format("Status:"));

        if (expectedStatus == null)
        {
            descriptionBuf.AppendLine(String.Format("  Source: {0}", actualStatus != null ? actualStatus.Source.ToString() : string.Empty));
            descriptionBuf.AppendLine(String.Format("  Tag: {0}", actualStatus != null ? actualStatus.Tag.ToString() : string.Empty));
        }
        else
        {
            if (actualStatus.Source == expectedStatus.Source)
                descriptionBuf.AppendLine(String.Format("  Source: {0}", actualStatus.Source));
            else
                descriptionBuf.AppendLine(String.Format("  Source: {0} [{1}]", actualStatus.Source, expectedStatus.Source));

            if (actualStatus.Tag == expectedStatus.Tag)
                descriptionBuf.AppendLine(String.Format("  Tag: {0}", actualStatus.Tag));
            else
                descriptionBuf.AppendLine(String.Format("  Tag: {0} [{1}]", actualStatus.Tag, expectedStatus.Tag));
        }

        //Always just show the original error, no need to validate it
        descriptionBuf.AppendLine(String.Format("  Error: {0}", actualStatus != null ? actualStatus.Error : string.Empty));
    }
  }
}
