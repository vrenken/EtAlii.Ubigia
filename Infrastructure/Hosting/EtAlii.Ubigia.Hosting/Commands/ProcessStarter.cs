namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using System.Diagnostics;
    using System.IO;

    class ProcessStarter : IProcessStarter
    {
        public void StartProcess(string fileName, string arguments = "")
        {
            fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = true,
            };
            Process.Start(startInfo);
        }
    }
}