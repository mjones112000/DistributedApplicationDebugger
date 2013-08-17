using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Communication
{
  public static class Communication
  {
    //public static List<string> ParseInputBuffer(List<Byte> buffer)
    //{
    //  int startIndex, endIndex;
    //  List<string> receivedMessages = new List<string>();

    //  //Get the index of the first SOH
    //  startIndex = buffer.IndexOf((byte)Protocal.SOH);

    //  if (startIndex > -1)
    //  {
    //    //Remove everything up until the SOH, anything before it is invalid
    //    buffer.RemoveRange(0, startIndex);
    //    //Get the index of the EOT
    //    endIndex = buffer.IndexOf((byte)Protocal.EOT);

    //    //Process message if an SOH and and EOT was found
    //    while (startIndex > -1 && endIndex > -1)
    //    {
    //      //Add the message to the list
    //      //receivedMessages.Add(Encoding.ASCII.GetString(buffer.ToArray(), 1, endIndex - 2));
    //      receivedMessages.Add(Encoding.ASCII.GetString(buffer.ToArray(), 1, endIndex - 1));
    //      //Remove everything up through the endIndex
    //      buffer.RemoveRange(0, endIndex);

    //      //Get the new SOH
    //      startIndex = buffer.IndexOf((byte)Protocal.SOH);

    //      if (startIndex > 0)
    //      {
    //        //Remove everything before the next SOH, it is useless
    //        buffer.RemoveRange(0, startIndex);
    //        //Get the next EOT
    //        endIndex = buffer.IndexOf((byte)Protocal.EOT);
    //      }
    //    }
    //  }

    //  return receivedMessages;
    //}

    //public static bool IsWindowsOS
    //{
    //  get
    //  {
    //    return
    //      !(Environment.OSVersion.Platform == PlatformID.MacOSX ||
    //        Environment.OSVersion.Platform == PlatformID.Unix ||
    //        Environment.OSVersion.Platform == PlatformID.Xbox);
    //  }
    //}
  }
}
