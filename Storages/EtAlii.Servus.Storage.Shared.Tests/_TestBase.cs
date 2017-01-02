//namespace EtAlii.Servus.Storage.Tests
//{
//    using EtAlii.Servus.Api;
//    using EtAlii.Servus.Storage;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using SimpleInjector;
//    using System;
//    using System.Diagnostics.CodeAnalysis;

//    [TestClass]
//    public abstract class TestBase
//    {
//        //protected IStorageConfiguration Configuration { get { return _configuration; } }
//        //private IStorageConfiguration _configuration;

//        //protected Container Container { get { return _container; } }
//        //private Container _container;

//        [TestInitialize]
//        public virtual void Initialize()
//        {
//            App.Setup<App>();

//            _container = App.Current.Container;
//            _configuration = Container.GetInstance<IStorageConfiguration>();
//        }

//        [TestCleanup]
//        public virtual void Cleanup()
//        {
//            _container = null;
//            _configuration = null;
//        }

//        protected EtAlii.Servus.Storage.ContainerIdentifier CreateSimpleContainerIdentifier(string id = null)
//        {
//            string[] paths;
//            if (!String.IsNullOrEmpty(id))
//            {
//                paths = new string[]
//                {
//                    id,
//                    Guid.NewGuid().ToString(),
//                };
//            }
//            else
//            {
//                paths = new string[]
//                {
//                    Guid.NewGuid().ToString(),
//                };
//            }
//            return EtAlii.Servus.Storage.ContainerIdentifier.FromPaths(paths);
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