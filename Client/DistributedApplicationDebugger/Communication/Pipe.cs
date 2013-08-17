using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DistributedApplicationDebugger.Communication
{
  public class Pipe
  {
    byte[] readArray = new byte[65535];

    public event EventHandler<MessageReadEventArgs> IncomingMessage;

    public Pipe(Stream from, Stream to)
    {
      From = from;
      To = to;
    }

    public void BeginStream()
    {
      ContinuousMode = true;
      try
      {
        From.BeginRead(readArray, 0, 65535, EndStreamRead, null);
      }
      catch (Exception ex)
      {
        ContinuousMode = false;
      }
    }

    public void EndStream()
    {
      ContinuousMode = false;
      From.Dispose();
    }

    private void EndStreamRead(IAsyncResult result)
    {
      int readBytes = 0;
      bool _readFailed = false;
      try
      {
        readBytes = From.EndRead(result);

      }
      catch (IOException ex)
      {
        string s = ex.ToString();
        _readFailed = true;
      }
      catch (ObjectDisposedException)
      {
        //If we've closed the stream we'll get a disposed exception.  Nothing to do with it.
        return;
      }

      if (_readFailed || readBytes == 0)
      {
        //DisconnectConnections();
        //Close();
        return;
      }
      else
      {
        if (IncomingMessage != null)
          IncomingMessage(this, new MessageReadEventArgs(Encoding.ASCII.GetString(readArray, 0, readBytes)));

        if(To == null)
        {
          if (ContinuousMode)
          {
            BeginStream();
          }
       }
       else
        To.BeginWrite(readArray, 0, readBytes, EndStreamWrite, null);
        

      }
    }

    private void EndStreamWrite(IAsyncResult result)
    {
      To.EndWrite(result);

      if (ContinuousMode)
        BeginStream();
    }

    private bool ContinuousMode
    {
      get;
      set;
    }

    private Stream From
    {
      get;
      set;
    }

    private Stream To
    {
      get;
      set;
    }
  }
}
