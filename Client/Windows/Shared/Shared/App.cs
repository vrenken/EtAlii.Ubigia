namespace EtAlii.Ubigia.Windows.Shared
{
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using EtAlii.xTechnology.MicroContainer;
    using System;
    using System.IO;
    using EtAlii.Ubigia.Windows.Settings;

    public abstract class App : System.Windows.Application
    {
        public const string StorageNaming = "Space";
        public const string StoragesNaming = "Spaces";

        public static string CurrentDirectory => _currentDirectory.Value;
        private static readonly Lazy<string> _currentDirectory = new Lazy<string>(Directory.GetCurrentDirectory);

        public static string ShellExtensionsDirectory => _shellExtensionsDirectory.Value;
        private static readonly Lazy<string> _shellExtensionsDirectory = new Lazy<string>(() => Path.Combine(CurrentDirectory, "ShellExtensions"));

        public new static App Current => System.Windows.Application.Current as App;

        public readonly Container Container;

        protected App()
        {
            Container = new Container();
            //Container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            Directory.CreateDirectory(ShellExtensionsDirectory);
        }

        protected virtual void RegisterKnownTypes()
        {
            var typesToInclude = new[]
            {
                typeof(BindableBase),
            };

            var typesToExclude = new[]
            {
                typeof(IGlobalSettings),
                typeof(StorageSettings),
                typeof(ILogger),
            };

            //RegisterKnownTypesInAssembly(Assembly.GetExecutingAssembly(), typesToInclude, typesToExclude);

            Container.Register<IGlobalSettings, GlobalSettings>();
            Container.Register<ILogFactory, DisabledLogFactory>();
            Container.Register(GetLogger);
            Container.Register<IProfilerFactory, DisabledProfilerFactory>();
            Container.Register(GetProfiler);
        }

        //protected void RegisterKnownTypesInAssembly(Assembly assembly, Type[] typesToInclude, Type[] typesToExclude)
        //{
        //    var typesToRegister = assembly.GetTypes()
        //                                  .Where(type => type.IsClass && !type.IsAbstract)
        //                                  .Where(type => !typesToExclude.Contains(type))
        //                                  .Where(type => !typesToExclude.Any(customRegisteredType => customRegisteredType.IsAssignableFrom(type)))
        //                                  .Where(type => typesToInclude.Any(type.IsSubclassOf));

        //    foreach (var typeToRegister in typesToRegister)
        //    {
        //        Container.Register(typeToRegister);
        //    }
        //}

        private ILogger GetLogger()
        {
            var factory = Container.GetInstance<ILogFactory>();
            return factory.Create("EtAlii", "EtAlii.Ubigia.Client.Windows");
        }

        private IProfiler GetProfiler()
        {
            var factory = Container.GetInstance<IProfilerFactory>();
            return factory.Create("EtAlii", "EtAlii.Ubigia.Client.Windows");
        }
    }
}
