using System.IO;
using System.Collections.Generic;

namespace DistributedApplicationDebugger.Communication
{
  public class StreamDataWrapper
  {
    public StreamDataWrapper(Stream stream, byte[] data)
    {
      Stream = stream;
      Data = data;
      InputBuffer = new List<byte>();
    }

    public Stream Stream
    {
      get;
      private set;
    }

    public byte[] Data
    {
      get;
      private set;
    }

    public List<byte> InputBuffer
    {
      get;
      private set;
    }
  }
}
