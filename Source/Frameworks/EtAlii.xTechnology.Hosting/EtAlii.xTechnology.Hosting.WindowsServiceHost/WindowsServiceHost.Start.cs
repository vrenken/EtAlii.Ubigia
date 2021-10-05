// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Security.Principal;

    public partial class WindowsServiceHost
    {
        public static void Start(string[] args, HostOptions options, ServiceDetails serviceDetails)
        {
            // Instantiate the service logic.
            var serviceLogic = new ServiceLogic(options);

            // No arguments? Run the Service and exit when service exits.
            if (!args.Any())
            {
#pragma warning disable CA2000 // We cannot dispose the service as its lifetime gets managed by ServiceBase.Run.
                var service = new WindowsService(serviceLogic, serviceDetails);
#pragma warning restore CA2000
                System.ServiceProcess.ServiceBase.Run(service);
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
                    InstallService(serviceDetails, args);
                    break;
                case "/U":
                case "/UNINSTALL":
                    UninstallService(serviceDetails, args);
                    break;
                default:
                    PrintUsage();
                    break;
            }
        }

        private static void InstallService(ServiceDetails service, string[] args)
        {
            if (IsAdministrator())
            {
                ServiceInstaller.Install(service);
            }
            else
            {
                RunElevated(args);
            }
        }

        private static void UninstallService(ServiceDetails service, string[] args)
        {
            if (IsAdministrator())
            {
                ServiceInstaller.Uninstall(service);
            }
            else
            {
                RunElevated(args);
            }
        }

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4834:Controlling permissions is security-sensitive",
            Justification = "Safe to do so here.")]
        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RunElevated(string[] args)
        {
            var fileName = Assembly.GetEntryAssembly()!.Location;
            var startInfo = new ProcessStartInfo(fileName, string.Join(" ", args))
            {
                UseShellExecute = true,
                Verb = "runas", // indicates to elevate privileges
            };

            using var process = new Process
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
            Console.WriteLine("- Press ENTER to stop - ");
            Console.In.ReadLine();

            serviceLogic.Stop();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine();
            Console.WriteLine(" /Start\t\tStart the service");
            Console.WriteLine(" /Install\tInstall the executable as Windows Service");
            Console.WriteLine(" /Uninstall\tUninstall the Windows Service");
        }
    }
}
