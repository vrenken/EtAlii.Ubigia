//namespace EtAlii.Servus.Storage.Ntfs.Tests
//{
//    using EtAlii.Servus.Storage;
//    using EtAlii.xTechnology.Logging;
//    using SimpleInjector;


//    public class App
//    {
//        public static App Current { get { return _current; } }
//        private static App _current;

//        public readonly Container Container;

//        public App()
//        {
//            Container = new Container();
//        }

//        public static void Setup<T>()
//            where T: App, new()
//        {
//            //if (_current == null)
//            //{
//                var app = new T();

//                app.RegisterTypes();
//                app.Container.Verify();

//                _current = app;
//            //}
//            //else if (_current.GetType() != typeof(T))
//            //{
//            //    throw new InvalidOperationException("Application setup is done twice with a different configuration");
//            //}
//        }

//        protected virtual void RegisterTypes()
//        {
//            Container.Register<ILogFactory, LogFactory>(Lifestyle.Singleton);
//            Container.Register<ILogger>(GetLogger, Lifestyle.Transient);
//            Container.RegisterSingle<IStorageSystem>(() => CreateStorageSystem());
//        }

//        private IStorageSystem CreateStorageSystem()
//        {
//            var configuration = new Configuration { Name = "Unit test storage - NTFS Storage" };

//            var diagnostics = new DiagnosticsConfiguration
//            {
//                EnableProfiling = false,
//                EnableLogging = false,
//                EnableDebugging = true,
//                CreateLogFactory = () => new LogFactory(),
//                CreateLogger = CreateLogger,
//                CreateProfilerFactory = null,
//                CreateProfiler = null,
//            };

//        return new StorageSystemFactory().Create<NtfsStorageSystem>(
//                            _configuration,
//                            _enableProfiling,
//                            _enableLogging,
//                            _enableDebugging,
//                            () => Container.GetInstance<IProfiler>(),
//                            () => Container.GetInstance<ILogger>());
//        }

//        private ILogger GetLogger()
//        {
//            var factory = Container.GetInstance<ILogFactory>();
//            return factory.Create("EtAlii", "EtAlii.Servus.Storage.Ntfs.Tests");
//        }
//    }
//}
