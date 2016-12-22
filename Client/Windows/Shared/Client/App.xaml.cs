namespace EtAlii.Servus.Client.Windows
{
    using EtAlii.Servus.Client.Windows.Shared;
    using EtAlii.Servus.Client.Windows.TaskbarIcon;
    using EtAlii.Servus.Client.Windows.UserInterface;
    using SimpleInjector;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : EtAlii.Servus.Client.Windows.Shared.App
    {
        public static new App Current { get { return System.Windows.Application.Current as App; } }

        public new MainWindow MainWindow { get { return base.MainWindow as MainWindow; } set { base.MainWindow = value; } }

        public static bool AlreadyRunning
        {
            get
            {
                var alreadyRunning = false;

                var current = Process.GetCurrentProcess();
                var processes = Process.GetProcessesByName(current.ProcessName);

                //Loop through the running processes in with the same name 
                foreach (var process in processes)
                {
                    //Ignore the current process 
                    if (process.Id != current.Id)
                    {
                        //Make sure that the process is running from the exe file. 
                        if (Assembly.GetExecutingAssembly().Location.
                             Replace("/", "\\") == current.MainModule.FileName)
                        {
                            alreadyRunning = true;
                            break;
                        }
                    }
                }
                return alreadyRunning;
            }
        }

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            if (AlreadyRunning)
            {
                App.Current.Shutdown();
            }
            else
            {
                RegisterKnownTypes();
                Container.Verify();

                StartServices();
            }
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            StopServices();
        }

        private void StartServices()
        {
            var services = Container.GetAllInstances<IApplicationService>();
            foreach (var service in services)
            {
                service.Start();
            }
        }

        private void StopServices()
        {
            var services = Container.GetAllInstances<IApplicationService>();
            foreach (var service in services)
            {
                service.Stop();
            }
        }

        protected override void RegisterKnownTypes()
        {
            base.RegisterKnownTypes();

            Container.RegisterInitializer<MainWindow>(window => window.DataContext = Container.GetInstance<MainWindowViewModel>());
            Container.RegisterInitializer<StorageWindow>(window => window.DataContext = Container.GetInstance<StorageSettingsViewModel>());
            Container.RegisterInitializer<TaskbarIcon.TaskbarIcon>(window => window.DataContext = Container.GetInstance<TaskbarIconViewModel>());

            Container.Register<ShellExtensionService, ShellExtensionService>(Lifestyle.Singleton);
            Container.Register<TaskbarIconService, TaskbarIconService>(Lifestyle.Singleton);
            Container.RegisterCollection<IApplicationService>(new [] { typeof(ShellExtensionService), typeof(TaskbarIconService) });
        }
    }
}
