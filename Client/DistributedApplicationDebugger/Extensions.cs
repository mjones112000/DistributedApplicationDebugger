using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DistributedApplicationDebugger.Extensions
{
  public static class Extensions
  {
    public static bool ConstainsCaseInsensitive(this string str1, string str2)
    {
      return str1.IndexOf(str2, StringComparison.CurrentCultureIgnoreCase) >= 0;
    }

    public static string ToReadableString(this TimeSpan span)
    {
      return String.Format("{0}:{1}:{2}:{3}",
                            span.Hours.ToString().PadLeft(2, '0'),
                            span.Minutes.ToString().PadLeft(2, '0'),
                            span.Seconds.ToString().PadLeft(2, '0'),
                            span.Milliseconds.ToString().PadLeft(2, '0').Substring(0, 2));
    }

    public static string Delimit(this IEnumerable<int> list, string delimiter)
    {
      StringBuilder commaDelimeted = new StringBuilder();
      if (list != null && list.Any())
      {
        commaDelimeted.Append(list.First().ToString());
        foreach (int item in list.Skip(1))
        {
          commaDelimeted.Append(String.Format("{0}{1}", delimiter, item.ToString()));
        }
      }

      return commaDelimeted.ToString();
    }
  }
}
