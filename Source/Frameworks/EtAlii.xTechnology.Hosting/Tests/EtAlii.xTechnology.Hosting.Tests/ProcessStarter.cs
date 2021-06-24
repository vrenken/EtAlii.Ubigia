// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests
{
    using System.Diagnostics;

    internal class ProcessStarter : IProcessStarter
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