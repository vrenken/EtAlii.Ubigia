//namespace EtAlii.Ubigia.Infrastructure.Hosting
//{
//    using EtAlii.Ubigia.Infrastructure.Functional;
//    using EtAlii.Ubigia.Storage;

//    public abstract class InfrastructureHostBase : IInfrastructureHost
//    {
//        public IHostConfiguration Configuration { get; }

//        public IInfrastructure Infrastructure { get; }

//        public IStorage Storage { get; }

//        protected InfrastructureHostBase(IHostConfiguration configuration, IInfrastructure infrastructure, IStorage storage)
//        {
//            Configuration = configuration;
//            Infrastructure = infrastructure;
//            Storage = storage;
//        }

//        public abstract void Start();
//        public abstract void Stop();
//    }
//}