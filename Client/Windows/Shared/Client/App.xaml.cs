namespace EtAlii.Ubigia.Windows.Client
{
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : EtAlii.Ubigia.Windows.Shared.App
    {
        public new static App Current => System.Windows.Application.Current as App;

        public new MainWindow MainWindow { get { return base.MainWindow as MainWindow; } set { base.MainWindow = value; } }

        private IApplicationService[] _services;

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
//
//        private void OnApplicationStartup(object sender, StartupEventArgs e)
//        [
//            if (AlreadyRunning)
//            [
//                Current.Shutdown()
//            ]
//            else
//            [
//                RegisterKnownTypes()
//                //Container.Verify()
//
//                StartServices()
//            ]
//        ]
//
//        private void OnApplicationExit(object sender, ExitEventArgs e)
//        [
//            StopServices()
//        ]
        private void StartServices()
        {
            _services = new IApplicationService[]
            {
                Container.GetInstance<IShellExtensionService>(),
                Container.GetInstance<ITaskbarIconService>()
            };
            foreach (var service in _services)
            {
                service.Start();
            }
        }

        private void StopServices()
        {
            foreach (var service in _services)
            {
                service.Stop();
            }

            _services = null;
        }

        protected override void RegisterKnownTypes()
        {
            base.RegisterKnownTypes();

            Container.RegisterInitializer<MainWindow>(window => window.DataContext = Container.GetInstance<MainWindowViewModel>());
            Container.RegisterInitializer<StorageWindow>(window => window.DataContext = Container.GetInstance<StorageSettingsViewModel>());
            Container.RegisterInitializer<TaskbarIcon>(window => window.DataContext = Container.GetInstance<TaskbarIconViewModel>());

            Container.Register<IShellExtensionService, ShellExtensionService>();
            Container.Register<ITaskbarIconService, TaskbarIconService>();
            //Container.RegisterCollection<IApplicationService>(new [] { typeof(ShellExtensionService), typeof(TaskbarIconService) })
        }
    }
}
