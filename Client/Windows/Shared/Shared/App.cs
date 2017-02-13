namespace EtAlii.Ubigia.Client.Windows.Shared
{
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using EtAlii.xTechnology.MicroContainer;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public abstract class App : System.Windows.Application
    {
        public const string StorageNaming = "Space";
        public const string StoragesNaming = "Spaces";

        public static string CurrentDirectory { get { return _currentDirectory.Value; } }
        private static readonly Lazy<string> _currentDirectory = new Lazy<string>(Directory.GetCurrentDirectory);

        public static string ShellExtensionsDirectory { get { return _shellExtensionsDirectory.Value; } }
        private static readonly Lazy<string> _shellExtensionsDirectory = new Lazy<string>(() => Path.Combine(CurrentDirectory, "ShellExtensions"));

        public static new App Current { get { return System.Windows.Application.Current as App; } }

        public readonly Container Container;

        protected App()
        {
            Container = new Container();
            //Container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            Directory.CreateDirectory(App.ShellExtensionsDirectory);
        }

        protected virtual void RegisterKnownTypes()
        {
            var typesToInclude = new Type[]
            {
                typeof(BindableBase),
            };

            var typesToExclude = new Type[]
            {
                typeof(IGlobalSettings),
                typeof(StorageSettings),
                typeof(ILogger),
            };

            //RegisterKnownTypesInAssembly(Assembly.GetExecutingAssembly(), typesToInclude, typesToExclude);

            Container.Register<IGlobalSettings, GlobalSettings>();
            Container.Register<ILogFactory, DisabledLogFactory>();
            Container.Register<ILogger>(GetLogger);
            Container.Register<IProfilerFactory, DisabledProfilerFactory>();
            Container.Register<IProfiler>(GetProfiler);
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
