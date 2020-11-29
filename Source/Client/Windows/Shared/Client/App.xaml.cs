namespace EtAlii.Ubigia.Windows.Client
{
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public new static App Current => System.Windows.Application.Current as App;

        public new MainWindow MainWindow { get => base.MainWindow as MainWindow; set => base.MainWindow = value; }

        //private IApplicationService[] _services

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
                    if (process.Id == current.Id) continue;
                    
                    //Make sure that the process is running from the exe file. 
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") != current.MainModule.FileName) continue;
                        
                    alreadyRunning = true;
                    break;
                }
                return alreadyRunning;
            }
        }

        protected override void RegisterKnownTypes()
        {
            base.RegisterKnownTypes();

            Container.RegisterInitializer<MainWindow>(window => window.DataContext = Container.GetInstance<MainWindowViewModel>());
            Container.RegisterInitializer<StorageWindow>(window => window.DataContext = Container.GetInstance<StorageSettingsViewModel>());
            Container.RegisterInitializer<TaskbarIcon>(window => window.DataContext = Container.GetInstance<TaskbarIconViewModel>());

            //Container.Register[IShellExtensionService, ShellExtensionService][]
            Container.Register<ITaskbarIconService, TaskbarIconService>();
            //Container.RegisterCollection<IApplicationService>(new [] [ typeof(ShellExtensionService), typeof(TaskbarIconService) ])
        }
    }
}
