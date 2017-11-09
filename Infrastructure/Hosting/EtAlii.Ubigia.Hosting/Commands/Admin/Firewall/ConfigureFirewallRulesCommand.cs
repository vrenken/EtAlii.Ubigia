namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public class ConfigureFirewallRulesCommand : HostCommandBase, IConfigureFirewallRulesCommand
    {
        public string Name => "Admin/Configure firewall rules";

        public void Execute()
        {
            TryConfigure();
        }

        protected override void OnHostStatusChanged(HostStatus status)
        {
            CanExecute = status == HostStatus.Stopped || status == HostStatus.Shutdown;
        }

        private Task<bool> TryConfigure()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                var scriptFullPath = Path.GetTempFileName();
                scriptFullPath = Path.ChangeExtension(scriptFullPath, "ps");
                var type = this.GetType();
                var assembly = type.Assembly;
                var assemblyName = assembly.GetName().Name;

                using (var resourceStream = assembly.GetManifestResourceStream(type, "Commands.Admin.Firewall.ConfigureFirewall.ps1"))
                using (var fileStream = File.Create(scriptFullPath))
                using (var reader = new StreamReader(resourceStream))
                using (var writer = new StreamWriter(fileStream))
                {
                    var content = reader.ReadLine();
                    writer.Write(content);
                }

                //var logFile = Path.GetTempFileName();
                var scriptArgs = new[]
                {
                    "-ServiceAssemblyName", assemblyName,
                    //  "-LogFile", logFile
                };

                var process = StartElevatedPowerShellScript(scriptFullPath, scriptArgs);
                process.WaitForExit();

                File.Delete(scriptFullPath);

                //Debug.WriteLine($"[{GetType().Name}] Firewall configuration script exit code: {proc.ExitCode}");
                //ProcessLogFile(logFile);

                taskCompletionSource.SetResult(process.ExitCode == 0);
            });

            return taskCompletionSource.Task;
        }

        private Process StartElevatedPowerShellScript(string scriptPath, params string[] scriptArgs)
        {
            const string powershell = "powershell.exe";

            var arguments = new[] {
                "-NoProfile",
                "-ExecutionPolicy", "ByPass",
                "-File", scriptPath
            }.Concat(scriptArgs).Select(QuoteArgument);

            var psArgumentsLine = string.Join(" ", arguments);

            //Debug.WriteLine($"[{GetType().Name}] Starting elevated process: {powershell} {psArgumentsLine}");

            var startInfo = new ProcessStartInfo
            {
                FileName = powershell,
                Arguments = psArgumentsLine,
                Verb = "runas", // Elevate process
                UseShellExecute = true, // Necessary for elevation
                WindowStyle = ProcessWindowStyle.Hidden, // Suppress PowerShell window
            };

            return Process.Start(startInfo);
        }

        private string QuoteArgument(string arg)
        {
            return !arg.Contains(' ') ? arg : '"' + arg + '"';
        }

        //private void ProcessLogFile(string logFile)
        //{
        //    if (File.Exists(logFile))
        //    {
        //        try
        //        {
        //            Debug.WriteLine($"[{GetType().Name}] Firewall configuration script output:");
        //            var log = File.ReadAllText(logFile);
        //            Debug.WriteLine(log);
        //        }
        //        finally
        //        {
        //            File.Delete(logFile);
        //        }
        //    }
        //}

    }
}
