namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.Ubigia.Api.Functional;

    public abstract class ProviderHostBase : IProviderHost
    {
        public IDataContext Data { get; }

        private readonly IProviderManager _providerManager;

        public IHostConfiguration Configuration { get; }

        protected ProviderHostBase(
            IDataContext data,
            IHostConfiguration configuration, 
            IProviderManager providerManager)
        {
            Data = data;
            Configuration = configuration;
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
