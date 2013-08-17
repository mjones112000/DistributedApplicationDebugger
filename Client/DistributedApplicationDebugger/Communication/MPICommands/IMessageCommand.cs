using System.Data;
using System.Collections.Generic;
namespace DistributedApplicationDebugger.Communication.MPICommands
{
  public interface IMessageCommand
  {
    int CommandId
    {
      get;
    }

    int? MessageId
    {
      get;
      set;
    }

    bool IsMatch(IMessageCommand command);

    IMessageCommand MatchingMessageCommand
    {
      get;
      set;
    }

    int NodeId
    {
      get;
    }

    int Count
    {
      get;
    }
    
    string Name
    {
      get;
    }

    int? Dest
    {
      get;
    }

    List<BufferValue> Buf
    {
      get;
      set;
    }

    List<BufferValue> InvalidBuf
    {
        get;
    }

    string DataType
    {
      get;
    }

    MPI_SOURCE Src
    {
      get;
    }
    
    MPI_TAG Tag
    {
      get;
    }

    int MessageLinkId
    {
      get;
    }

    MessageDisplayState DisplayState
    {
      get;
      set;
    }

    DataRow MessageDataRow
    {
      get;
      set;
    }

    int? BufferCommandId
    {
      get;
    }

  }
}
