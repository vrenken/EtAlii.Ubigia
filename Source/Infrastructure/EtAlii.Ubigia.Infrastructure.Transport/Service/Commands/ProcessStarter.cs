﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Diagnostics;

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