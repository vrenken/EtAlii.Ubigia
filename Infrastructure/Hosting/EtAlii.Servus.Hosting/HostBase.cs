namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;

    public abstract class HostBase : IHost
    {
        public IHostConfiguration Configuration { get { return _configuration; } }
        private readonly IHostConfiguration _configuration;

        public IInfrastructure Infrastructure { get { return _infrastructure; } }
        private readonly IInfrastructure _infrastructure;

        public IStorage Storage { get { return _storage; } }
        private readonly IStorage _storage;

        protected HostBase(IHostConfiguration configuration, IInfrastructure infrastructure, IStorage storage)
        {
            _configuration = configuration;
            _infrastructure = infrastructure;
            _storage = storage;
        }

        public abstract void Start();
        public abstract void Stop();
    }
}