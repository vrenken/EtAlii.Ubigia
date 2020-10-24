namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
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