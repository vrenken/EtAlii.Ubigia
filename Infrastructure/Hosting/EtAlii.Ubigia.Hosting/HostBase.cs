namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;

    public abstract class HostBase : IHost
    {
        public IHostConfiguration Configuration => _configuration;
        private readonly IHostConfiguration _configuration;

        public IInfrastructure Infrastructure => _infrastructure;
        private readonly IInfrastructure _infrastructure;

        public IStorage Storage => _storage;
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