//-----------------------------------------------------------------------
// <copyright file="FileAttributesTools.cs" author="Elad Karako" company="no">
//     UnLicensed (Public Domain).
//     Date: 07/01/2012
//     Time: 03:18
// </copyright>
// <summary>
// Extracting some information from a file.
// </summary>
//-----------------------------------------------------------------------

namespace ConsoleBase64
{
  using ConsoleBase64;
  using System;
  using System.Collections.Generic;
  using System.Security.Policy;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Linq;
  using System.IO;
  using System.Security.Cryptography;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  /// <summary>
  /// Description of FileAttributesTools.
  /// </summary>
  public class FileAttributesTools
  {
    /// <summary>
    /// External library header for formatting byte-size.
    /// </summary>
    /// <param name="fileSize">Will be filled with file-size (bytes).</param>
    /// <param name="buffer">The parameter is not used.</param>
    /// <param name="bufferSize">The parameter is not used.</param>
    /// <returns>Success-indicator of the action.</returns>
    [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
    public static extern long StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);

    /// <summary>
    /// Using the external library, for byte-size formatting.
    /// </summary>
    /// <param name="lSize">File-size (bytes).</param>
    /// <returns>Formatted-size.</returns>
    public static string getPrettyFileSize(long lSize)
    {
      StringBuilder sb = new StringBuilder(12);
      StrFormatByteSize(lSize, sb, sb.Capacity);
      return sb.ToString();
    }
  }

}
