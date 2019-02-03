namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public class ConfigureFirewallRulesCommand : SystemCommandBase, IConfigureFirewallRulesCommand
    {
        public string Name => "Admin/Configure firewall rules";

        public ConfigureFirewallRulesCommand(ISystem system)
            : base(system)
        {
        }

        public void Execute()
        {
            TryConfigure();
        }

        protected override void OnSystemStateChanged(State state)
        {
            CanExecute = state == State.Stopped || state == State.Shutdown;
        }

        private Task<bool> TryConfigure()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                var scriptFullPath = Path.GetTempFileName();
                scriptFullPath = Path.ChangeExtension(scriptFullPath, "ps1");
                var type = GetType();
                var assembly = type.Assembly;
                var assemblyName = assembly.GetName().Name;

                Stream resourceStream = null; 
                FileStream fileStream = null;
                try
                {
                    resourceStream = assembly.GetManifestResourceStream(type, "Commands.Admin.Firewall.ConfigureFirewall.ps1");
                    fileStream = File.Create(scriptFullPath);
                    using (var reader = new StreamReader(resourceStream))
                    using (var writer = new StreamWriter(fileStream))
                    {
                        resourceStream = null;
                        fileStream = null;
                        var content = reader.ReadToEnd();
                        writer.Write(content);
                    }
                }
                finally
                {
                    resourceStream?.Dispose();
                    fileStream?.Dispose();
                }
                

                var logFile = Path.GetTempFileName();
                var scriptArgs = new[]
                {
                    //"-ServicePort", _infrastructure.Configuration.Address.Split(':').Last(),
                    "-ServicePort", "UKNOWN",
                    "-ServiceAssemblyName", assemblyName,
                    //"-RuleDisplayName", $"EtAlii Infrastructure Service ({_infrastructure.Configuration.Name})",
                    "-RuleDisplayName", $"EtAlii Infrastructure Service (UNKNOWN)",
                    "-LogFile", logFile
                };

                var process = StartElevatedPowerShellScript(scriptFullPath, scriptArgs);
                process.WaitForExit();

                ProcessLogFile(logFile);

                taskCompletionSource.SetResult(process.ExitCode == 0);

                File.Delete(scriptFullPath);
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

        private void ProcessLogFile(string logFile)
        {
            if (File.Exists(logFile))
            {
                try
                {
                    //Debug.WriteLine($"[{GetType().Name}] Firewall configuration script output:");
                    //var log = File.ReadAllText(logFile);
                    //Debug.WriteLine(log);
                }
                finally
                {
                    File.Delete(logFile);
                }
            }
        }

    }
}
