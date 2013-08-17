using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
  public static class Extensions
  {
    public static bool ConstainsCaseInsensitive(this string str1, string str2)
    {
      return str1.IndexOf(str2, StringComparison.CurrentCultureIgnoreCase) >= 0;
    }
  }
}
