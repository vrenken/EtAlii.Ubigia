namespace EtAlii.Ubigia.Infrastructure.Hosting.WindowsServiceHost
{
    using System;
    using System.Collections;
    using System.Configuration.Install;
    using System.Linq;
    using System.ServiceProcess;

    [System.ComponentModel.DesignerCategory("Code")]
    public class ServiceInstaller : Installer
    {
        public static void Install(IServiceLogic service)
        {
            if (HasService(service.Name))
            {
                Console.WriteLine("Error: Service '{0}' has already been installed", service.Name);
                Console.WriteLine("- Press any key to continue - ");
                Console.ReadKey();
            }
            else
            {
                var installer = GetInstaller(service);
                installer.Install(new Hashtable());

                if (!HasService(service.Name))
                {
                    Console.WriteLine("Error: Service '{0}' failed to install", service.Name);
                    Console.WriteLine("- Press any key to continue - ");
                    Console.ReadKey();
                }
            }
        }

        public static void Uninstall(IServiceLogic service)
        {
            if (!HasService(service.Name))
            {
                Console.WriteLine("Error: Service '{0}' was not found", service.Name);
                Console.WriteLine("- Press any key to continue - ");
                Console.ReadKey();
            }
            else
            {
                var installer = GetInstaller(service);
                installer.Uninstall(null);

                if (HasService(service.Name))
                {
                    Console.WriteLine("Error: Service '{0}' failed to uninstall", service.Name);
                    Console.WriteLine("- Press any key to continue - ");
                    Console.ReadKey();
                }
            }
        }

        private static bool HasService(string serviceName)
        {
            var hasService = false;

            var serviceController = ServiceController
                .GetServices()
                .FirstOrDefault(s => s.ServiceName == serviceName);
            if (serviceController != null)
            {
                serviceController.Dispose();
                hasService = true;
            }
            return hasService;
        }

        public ServiceInstaller(IServiceLogic service)
        {
            var serviceProcessInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            // Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            // Service Information
            serviceInstaller.ServiceName = service.Name;
            serviceInstaller.DisplayName = service.DisplayName;
            serviceInstaller.Description = service.Description;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.AfterInstall += (o, e) =>
            {
                using (var serviceController = new ServiceController(serviceInstaller.ServiceName))
                {
                    serviceController.Start();
                }
            };

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }

        private static TransactedInstaller GetInstaller(IServiceLogic service)
        {
            var serviceAssembly = service.GetType().Assembly;

            var commandLine = new[] { String.Format("/assemblypath={0}", serviceAssembly.Location) };
            var logFile = string.Format("{0}.installlog", serviceAssembly.FullName);

            var installContext = new InstallContext(logFile, commandLine);
            var transactedInstaller = new TransactedInstaller();

            transactedInstaller.Installers.Add(new ServiceInstaller(service));
            transactedInstaller.Context = installContext;

            return transactedInstaller;
        }
    }
}
