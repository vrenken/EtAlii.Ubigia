namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using System.Diagnostics;
    using System.IO;

    class ProcessStarter : IProcessStarter
    {
        public void StartProcess(string folder, string fileName, string arguments = "")
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = folder,
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = true,
            };
            Process.Start(startInfo);
        }
    }
}