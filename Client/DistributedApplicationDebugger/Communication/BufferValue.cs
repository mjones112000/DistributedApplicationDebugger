using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistributedApplicationDebugger.Communication
{
  public class BufferValue
  {
    private string _hexString = null;
   
    public BufferValue(int index, string value)
    {
      Index = index;
      Value = value;
    }

    public int Index
    {
      get;
      private set;
    }

    public string Value
    {
      get;
      private set;
    }

    public string HexValue
    {
      get
      {
        if (_hexString == null)
        {
          char[] chars = Value.ToCharArray();
          StringBuilder stringBuilder = new StringBuilder();
          foreach (char c in chars)
          {
            stringBuilder.Append(((Int16)c).ToString("x"));
          }
          _hexString = stringBuilder.ToString();
        }

        return _hexString;
      }
    }
  }
}
