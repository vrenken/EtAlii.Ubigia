//namespace EtAlii.Ubigia.Hosting
//{
//    using EtAlii.xTechnology.Logging;
//    using EtAlii.Ubigia.Infrastructure.Functional;
//    using EtAlii.xTechnology.Hosting;

//    public class PowerShellTestHost : HostBase, IHost
//    {
//        public IHostConfiguration Configuration { get; }

//        public IInfrastructure Infrastructure { get; }

//        private readonly ILogger _logger;

//        public PowerShellTestHost(
//            ISystemManager systemManager,
//            IHostConfiguration configuration,
//            IInfrastructure infrastructure,
//            ILogger logger)
//            : base(systemManager)
//        {
//            Configuration = configuration;
//            Infrastructure = infrastructure;
//            _logger = logger;
//        }
//    }
//}