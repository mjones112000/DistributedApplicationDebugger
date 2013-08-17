using System;
using System.Collections.Generic;
using System.Text;

namespace SSHLib
{
  public abstract class Utilities
  {
    public static readonly string GDB_FILE_NAME = "GDBBridge.c";
    public static readonly string GDB_COMPILED_NAME = "gdbBridge";

    #region StripUnixControlChars
    public static string StripUnixControlChars(string input)
    {
      StringBuilder stringBuilder = new StringBuilder();


      bool strip = false;
      for (int i = 0; i < input.Length; i++)
      {
        if (strip)
        {
          if (input[i] == 'm')
          {
            strip = false;
          }
        }
        else if (input[i] == 27)
        {
          strip = true;
        }
        else
        {
          stringBuilder.Append(input[i]);
        }
      }
      return stringBuilder.ToString();
    }
    #endregion StripUnixControlChars

    #region LocalPathDelimiter
    public static string LocalPathDelimiter
    {
      get
      {
        //if (Environment.CurrentDirectory.Contains("/"))
        //  return "/";
        //else
        //  return "\\";
        if (Environment.CurrentDirectory.Contains("\\"))
          return "\\";
        else
          return "/";
      }
    }
    #endregion LocalPathDelimiter

    #region ConfigFile
    public static string ConfigFile
    {
      get
      {
        return String.Format("{0}{1}{2}",
               Environment.CurrentDirectory, Utilities.LocalPathDelimiter, "debugger.config");
      }
    }
    #endregion ConfigFile

    #region LogFile
    public static string LogFile
    {
      get
      {
        return String.Format("{0}{1}{2}",
               Environment.CurrentDirectory, Utilities.LocalPathDelimiter, "debugger.log");
      }
    }
    #endregion LogFile
  }
}
