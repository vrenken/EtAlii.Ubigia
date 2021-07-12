// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class ConfigureFirewallRulesCommandBase : SystemCommandBase, IConfigureFirewallRulesCommand
    {
        public abstract string Name { get; }

        public abstract string ScriptResourceName { get; }

        protected ConfigureFirewallRulesCommandBase(ISystem system)
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

        private void TryConfigure()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                var scriptFullPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                scriptFullPath = Path.ChangeExtension(scriptFullPath, "ps1");
                var type = GetType();
                var assembly = type.Assembly;
                var assemblyName = assembly.GetName().Name;

                using var resourceStream = assembly.GetManifestResourceStream(type, ScriptResourceName);
                using var fileStream = File.Create(scriptFullPath);
                // ReSharper disable once AssignNullToNotNullAttribute
                using var reader = new StreamReader(resourceStream, leaveOpen:true);
                using var writer = new StreamWriter(fileStream, leaveOpen: true);

                var content = reader.ReadToEnd();
                writer.Write(content);

                var logFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                var scriptArgs = new[]
                {
                    "-ServicePort", "UNKNOWN",
                    "-ServiceAssemblyName", assemblyName,
                    "-RuleDisplayName", $"EtAlii Infrastructure Service (UNKNOWN)",
                    "-LogFile", logFile
                };

                var process = StartElevatedPowerShellScript(scriptFullPath, scriptArgs);
                process.WaitForExit();

                ProcessLogFile(logFile);

                taskCompletionSource.SetResult(process.ExitCode == 0);

                File.Delete(scriptFullPath);
            });
        }

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4036:Make sure the 'PATH' used to find this command includes only what you intend",
            Justification = "Safe to do so here, we just select the default Powershell in a non-primary process")]
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
                    //Debug.WriteLine($"[[GetType().Name]] Firewall configuration script output:")
                    //var log = File.ReadAllText(logFile)
                    //Debug.WriteLine(log)
                }
                finally
                {
                    File.Delete(logFile);
                }
            }
        }

    }
}
