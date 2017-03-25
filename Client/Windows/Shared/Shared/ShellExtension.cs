using System;
using System.Diagnostics;

namespace EtAlii.Ubigia.Client.Windows.Shared
{
    public static class ShellExtension
    {
        public static string FileName { get; set; } = "ShellExtension.dll";

        public static bool IsRegistered
        {
            get
            {
                var type = Type.GetTypeFromProgID(Identifiers.ProgramRegistrationString);
                return type != null;
            }
        }
        
        public static void ReloadWindowsExplorers()
        {
            // based on http://stackoverflow.com/questions/2488727/refresh-windows-explorer-in-win7
            var CLSID_ShellApplication = new Guid("13709620-C279-11CE-A49E-444553540000");
            var shellApplicationType = Type.GetTypeFromCLSID(CLSID_ShellApplication, true);

            dynamic shellApplication = Activator.CreateInstance(shellApplicationType);
            dynamic windows = shellApplication.Windows;
            int count = windows.Count;

            for (int i = 0; i < (int)count; i++)
            {
                dynamic item = windows.Item(i);

                // only refresh windows explorers
                if (item.Name == "Windows Explorer" ||
                    item.Name == "File Explorer")
                {
                    item.Refresh();
                }
            }
        }

        public static void Register()
        {
            string parameters = $"-i {FileName}";
            Execute("RegisterExtensionDotNet40.exe", parameters);
        }

        public static void Unregister()
        {
            string parameters = $"-u {FileName}";
            Execute("RegisterExtensionDotNet40.exe", parameters);
        }

        public static void RestartExplorer()
        {
            Execute("RestartExplorer.exe");
        }

        private static void Execute(string fileName, string arguments = null)
        {
            var processStartInfo = new ProcessStartInfo(fileName);
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Verb = "runas";
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.Arguments = arguments;
            var process = Process.Start(processStartInfo);
            process.WaitForExit();
        }

    }
}
