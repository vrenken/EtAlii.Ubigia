//namespace EtAlii.Ubigia.Infrastructure
//{
//    using System.Linq
//    using EtAlii.Ubigia.Infrastructure.Functional
//    using EtAlii.Ubigia.Infrastructure.Logical
//    using EtAlii.Ubigia.Infrastructure.Transport
//    using EtAlii.xTechnology.Logging
//    //using Microsoft.Owin.Testing
//    using EtAlii.xTechnology.MicroContainer
//    using EtAlii.xTechnology.Hosting.AspNetCore
//    using Microsoft.AspNetCore.Hosting
//    using Microsoft.AspNetCore.TestHost
//    using Microsoft.Extensions.Configuration

//	public class TestInfrastructure : IInfrastructure// WebApiInfrastructure
//    {
//        public IInfrastructureConfiguration Configuration { get; }

//        public ISpaceRepository Spaces { get; }

//        public IIdentifierRepository Identifiers { get; }

//        public IEntryRepository Entries { get; }

//        public IRootRepository Roots { get; }

//        public IAccountRepository Accounts { get; }

//        public IContentRepository Content { get; }

//        public IContentDefinitionRepository ContentDefinition { get; }

//        public IPropertiesRepository Properties { get; }

//        public IStorageRepository Storages { get; }

//        public TestServer Server { get; private set; }

//        private readonly Container _container
//        private readonly ILogger _logger
//        private readonly ILogicalContext _logicalContext
//        private IComponentManager[] _componentManagers

//        public TestInfrastructure(
//            Container container,
//            IInfrastructureConfiguration configuration, 
//            ISpaceRepository spaces, 
//            IIdentifierRepository identifiers, 
//            IEntryRepository entries, 
//            IRootRepository roots, 
//            IAccountRepository accounts, 
//            IContentRepository content, 
//            IContentDefinitionRepository contentDefinition,
//            IPropertiesRepository properties,
//            IStorageRepository storages,
//            ILogger logger,
//            ILogicalContext logicalContext)
//        {
//            Configuration = configuration
//            Identifiers = identifiers
//            Entries = entries
//            Roots = roots
//            Content = content
//            ContentDefinition = contentDefinition
//            Properties = properties

//            Storages = storages
//            Accounts = accounts
//            Spaces = spaces

//            _container = container
//            _logger = logger
//            _logicalContext = logicalContext
//        }

//        public void Start()
//        {
//	  //      var configuration = (IConfiguration) null
//			//var infrastructureService = new InfrastructureService(configuration)


//			// This action is needed because the Logical layer needs a fully functional system connection to do 
//			// the initialization of the storage and spaces.
//			// The functional is the only one that can provide these kind of connections.
//			Configuration.SystemConnectionCreationProxy.Initialize(() =>
//            {
//                var configuration = new SystemConnectionConfiguration()
//                    .Use(SystemTransportProvider.Create(this))
//                    .Use(this)
//                return new SystemConnectionFactory().Create(configuration)
//            })

//            _logicalContext.Start()

//            _logger.Info("Starting test infrastructure hosting")

//            _componentManagers = Configuration.ComponentManagerFactories
//                .Select(componentManagerFactory => componentManagerFactory(_container, Configuration.ComponentFactories))
//                .Cast<IComponentManager>()
//                .ToArray()

//	        var builder = new WebHostBuilder()
//		        .UseContentRoot(@"C:\WritingCommisions\VSMagazine\TestServer1\src\WebApplication1\WebApplication1")
//		        .UseEnvironment("Development")
//		        //.UseStartup<WebApplication1.Startup>()
//		        //.UseApplicationInsights()

//			Server = new TestServer(builder)

//	        //applicationBuilder =>
//	        //{
//		       // foreach (var componentManager in _componentManagers)
//		       // {
//			      //  componentManager.Start(applicationBuilder)
//		       // }
//	        //})

//            _logger.Info("Started test infrastructure hosting")
//        }

//        public void Stop()
//        {
//            _logger.Info("Stopping test infrastructure hosting")

//            Server.Dispose()
//            Server = null

//            foreach (var componentManager in _componentManagers)
//            {
//                componentManager.Stop()
//            }

//            _logger.Info("Stopped test infrastructure hosting")

//            _logicalContext.Stop()
//        }
//    }
//}
