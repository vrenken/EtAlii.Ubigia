//namespace EtAlii.Servus.Storage.Tests
//{
//    using EtAlii.Servus.Storage;
//    using EtAlii.Servus.Storage.InMemory;
//    using EtAlii.xTechnology.Logging;
//    using SimpleInjector;

//    public class App
//    {
//        public static App Current { get { return _current; } }
//        private static App _current;

//        public App()
//        {
//        }

//        public static void Setup<T>()
//            where T: App, new()
//        {
//            _current = new T();
//        }

//        private IStorageSystem GetStorageSystem()
//        {
//            var configuration = new Configuration { Name = "Unit test storage - Storage" };

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

//            return new StorageSystemFactory().Create<InMemoryStorageSystem>(configuration, diagnostics);
//        }

//        private ILogger CreateLogger(ILogFactory factory)
//        {
//            return factory.Create("EtAlii", "EtAlii.Servus.Storage.Ntfs.Tests");
//        }
//    }
//}
