namespace EtAlii.Ubigia.PowerShell.Tests
{
    using System.Diagnostics;
    using System.IO;


	public class Program
    {
        public static void Main2(string[] args)
        {
            string scriptName = Path.Combine(Directory.GetCurrentDirectory(), "TestScript.ps1");
            var process = Process.Start(@"C:\Program Files (x86)\PowerGUI\ScriptEditor.exe", scriptName);
            process.WaitForExit();
        }
    }
}
