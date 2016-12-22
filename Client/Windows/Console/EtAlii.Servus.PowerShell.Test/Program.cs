namespace EtAlii.Servus.PowerShell.Tests
{
    using System.Diagnostics;
    using System.IO;

    
    class Program
    {
        static void Main(string[] args)
        {
            string scriptName = Path.Combine(Directory.GetCurrentDirectory(), "TestScript.ps1");
            var process = Process.Start(@"C:\Program Files (x86)\PowerGUI\ScriptEditor.exe", scriptName);
            process.WaitForExit();
        }
    }
}
