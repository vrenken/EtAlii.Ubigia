namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.Ubigia.Api.Functional;

    public abstract class ProviderHostBase : IProviderHost
    {
        public IDataContext Data { get { return _data; } }
        private readonly IDataContext _data;

        private readonly IProviderManager _providerManager;

        public IHostConfiguration Configuration { get { return _configuration; } }
        private readonly IHostConfiguration _configuration;

        protected ProviderHostBase(
            IDataContext data,
            IHostConfiguration configuration, 
            IProviderManager providerManager)
        {
            _data = data;
            _configuration = configuration;
            _providerManager = providerManager;
        }

        public virtual void Start()
        {
            _providerManager.Start();
        }

        public virtual void Stop()
        {
            _providerManager.Stop();
        }
    }
}
