//-----------------------------------------------------------------------
// <copyright file="Program.cs" author="Elad Karako" company="no">
//     UnLicensed (Public Domain).
//     Date: 06/01/2012
//     Time: 19:33
// </copyright>
// <summary>
// Main command-line argument-handler.
// </summary>
//-----------------------------------------------------------------------

namespace ConsoleBase64
{
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

  class Program
  {
    public static bool bDecode = false;
    //-decode
    public static bool is_encode = false;
    //-encode
    public static bool bInFile = false;
    //-infile "c:\123.css" (contains plain text or encoded text)
    public static bool bInText = false;
    //-intext abcdefg or -intext YWJjZGVmZw==
    public static bool is_wrap = false;
    //-wrap -> will auto add: data:text/___mine_type___;base64,...
    public static bool is_copy = false;
    //-copy -> will also copy the result to the Clipboard (copy&paste)
    public static bool bVerbose = false;
    //-verbose -> will write more data into the screen, not good if you want to stream the result to file.
    public static bool bQuite = false;
    //-quite -> will not write anything to screen, useful if you want only result to copy to Clipboard, or write to file without also showing on screen, by defult result is written to screen.
    public static bool bOutFile = false;
    //-outfile "c:\123.txt" will write the result to file, and show on screen, can combined with -quite to only write to file.
    public static bool bNoErrors = false;
    //-noerrors will not show even the Error: messages
    public static bool bHelp = false;
    //-help --help -h /?

    
    public static void log2me(string s)
    {
      if (s.Contains("Error:")) {
        if (bNoErrors) {
          return;
        } else {
          Console.WriteLine(s);
          return;
        }
      }
      
      if (bQuite)
        return;
      
      if (s.Contains("Information:")) {
        if (bVerbose) {
          Console.WriteLine(s);
          return;
        } else
          return;
      }
      //else - normal text
      if (bVerbose)
        Console.WriteLine("standard output: " + s);
      else
        Console.WriteLine(s);
    }

    
    [STAThread]
    public static void Main(string[] args)
    {
      
      
      string inFileName = "";
      string inFileExt = "";
      string inFileMineType = "";
      
      for (int i = 0; i < args.Length; i++) {
        if (args[i].Equals("-decode", StringComparison.CurrentCultureIgnoreCase)) {
          bDecode = true;
          continue;
        }
        if (args[i].Equals("-encode", StringComparison.CurrentCultureIgnoreCase)) {
          is_encode = true;
          continue;
        }
        if (args[i].Equals("-infile", StringComparison.CurrentCultureIgnoreCase)) {
          bInFile = true;
          continue;
        }
        if (args[i].Equals("-intext", StringComparison.CurrentCultureIgnoreCase)) {
          bInText = true;
          continue;
        }
        if (args[i].Equals("-wrap", StringComparison.CurrentCultureIgnoreCase)) {
          is_wrap = true;
          continue;
        }
        if (args[i].Equals("-copy", StringComparison.CurrentCultureIgnoreCase)) {
          is_copy = true;
          continue;
        }
        if (args[i].Equals("-verbose", StringComparison.CurrentCultureIgnoreCase)) {
          bVerbose = true;
          continue;
        }
        if (args[i].Equals("-quite", StringComparison.CurrentCultureIgnoreCase)) {
          bQuite = true;
          continue;
        }
        if (args[i].Equals("-outfile", StringComparison.CurrentCultureIgnoreCase)) {
          bOutFile = true;
          continue;
        }
        if (args[i].Equals("-noerrors", StringComparison.CurrentCultureIgnoreCase)) {
          bNoErrors = true;
          continue;
        }

        if (args[i].Equals("--help", StringComparison.CurrentCultureIgnoreCase)
            || args[i].Equals("-help", StringComparison.CurrentCultureIgnoreCase)
            || args[i].Equals("-h", StringComparison.CurrentCultureIgnoreCase)
            || args[i].Equals("/?", StringComparison.CurrentCultureIgnoreCase)) {
          
          bHelp = true;
          continue;
        }
      }
      //if there is no switch
      if (!(bDecode || is_encode || bInFile || bInText || is_wrap || is_copy || bVerbose || bQuite || bOutFile || bHelp))
        bHelp = true;
      
      if (bHelp) {    //no argument or varios help asks.
        log2me("Error: helping document");
        return;
      }
      
      if (bDecode && is_encode) { //no encode with decode
        log2me("Error: no -decode & -encode in the same run, select one.");
        return;
      }

      if (bInFile && bInText) { //no in file with in text
        log2me("Error: no -infile & -intext in the same run, select one.");
        return;
      }

      if (!bInFile && is_wrap) {
        log2me("Error: no -wrap works with -infile (not -intext for now...)");
        return;
      }
      
      if (bInFile) {    // get the file of the -infile, and make sure its ok.
        try {
          inFileName = Regex.Split(string.Join(" ", args), "-infile ")[1].Split(' ')[0].Replace("\"", "");
        } catch (Exception e) {
          log2me("Error: " + e.ToString() + "\n\n some error while trying to split the -infile file name argument");
        }

        if ((inFileName == null) || (inFileName.Length == 0)) {
          log2me("Error: can't find the argument of -infile, write again look the help with the examples again.");
        }
        FileInfo f = new FileInfo(inFileName);
        if (!f.Exists) {
          log2me("Error: can't find file in the argument of -infile. make sure the path is right.");
          return;
        }

        inFileExt = f.Extension;
        inFileMineType = (inFileExt.Length == 0 || inFileExt.Equals(".")) ? EncodeDecodeTools.getMineTypeByExtension("txt") : EncodeDecodeTools.getMineTypeByExtension(inFileExt.Substring(1));
        
        log2me("Information: " + f.FullName + "\n\tfile exist=true. \n\tattributes: " + f.Attributes.ToString() + "\n\tfile extension: " + f.Extension + "\n\tfile Mine-Type: " + inFileMineType + "\n\tcreation time: " + f.CreationTime + "\n\tlast write-time: " + f.LastWriteTime + "\n\tfile size: " + FileAttributesTools.getPrettyFileSize(f.Length));

        if (is_encode) {
          string sEncodedFile = EncodeDecodeTools.encodeFile(inFileName);
          
          // change the text to have nice wrapping around
          if (is_wrap)
            sEncodedFile = EncodeDecodeTools.wrapEncodedString(sEncodedFile, inFileMineType);
          
          // copy the content
          if (is_copy)
            EncodeDecodeTools.copyToClipboard(sEncodedFile);
          
          log2me(sEncodedFile);
        }
        
        if (bDecode) {
          
        }
      }
      
      

      
    }
  }
}
