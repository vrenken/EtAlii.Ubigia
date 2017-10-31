namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;
    using System.ServiceProcess;

    public static class Program
    {
        public static void Main(string[] args)
        {
            // Instantiate the service logic.
            var serviceLogic = new ServiceLogic();

            // No arguments? Run the Service and exit when service exits.
            if (!args.Any())
            {
                ServiceBase.Run(new WindowsService(serviceLogic));
                return;
            }

            // Parse arguments.
            switch (args.First().ToUpperInvariant())
            {
                case "/S":
                case "/START":
                    StartService(serviceLogic, args);
                    break;
                case "/I":
                case "/INSTALL":
                    InstallService(serviceLogic, args);
                    break;
                case "/U":
                case "/UNINSTALL":
                    UninstallService(serviceLogic, args);
                    break;
                default:
                    PrintUsage();
                    break;
            }
        }

        private static void InstallService(IServiceLogic myService, string[] args)
        {
            if (IsAdministrator())
            {
                ServiceInstaller.Install(myService);
            }
            else
            {
                RunElevated(args);
            }
        }

        private static void UninstallService(IServiceLogic myService, string[] args)
        {
            if (IsAdministrator())
            {
                ServiceInstaller.Uninstall(myService);
            }
            else
            {
                RunElevated(args);
            }
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RunElevated(string[] args)
        {
            var fileName = Assembly.GetEntryAssembly().Location;
            var startInfo = new ProcessStartInfo(fileName, String.Join(" ", args))
            {
                UseShellExecute = true,
                Verb = "runas", // indicates to elevate privileges
            };

            var process = new Process
            {
                EnableRaisingEvents = true, // enable WaitForExit()
                StartInfo = startInfo
            };
            process.Start();
            process.WaitForExit(); // sleep calling process thread until evoked process exit
        }

        /// <summary>
        /// Passes the remainder of the commandline arguments to the ServiceLogic and starts it.            
        /// </summary>
        /// <param name="serviceLogic"></param>
        /// <param name="args"></param>
        private static void StartService(IServiceLogic serviceLogic, string[] args)
        {
            serviceLogic.Start(args.Skip(1));

            Console.WriteLine();
            Console.WriteLine("- Press any key to stop - ");
            Console.ReadKey();

            serviceLogic.Stop();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine();
            Console.WriteLine(" /Start\t\tStart the service logic");
            Console.WriteLine(" /Install\tInstall the executable as Windows Service");
            Console.WriteLine(" /Uninstall\tUninstall the Windows Service");
        }
    }
}
