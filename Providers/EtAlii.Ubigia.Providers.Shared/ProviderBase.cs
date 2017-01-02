namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional;

    public abstract class ProviderBase : IProvider
    {
        public IDataContext Data { get { return _data; } }
        private readonly IDataContext _data;

        private readonly ComponentManager _componentManager;

        public IProviderConfiguration Configuration { get { return _configuration; } }
        private readonly IProviderConfiguration _configuration;

        protected ProviderBase(
            IDataContext data,
            IProviderConfiguration configuration, 
            ComponentManager componentManager)
        {
            _data = data;
            _configuration = configuration;
            _componentManager = componentManager;
        }

        public virtual void Start()
        {
            _componentManager.Start();
        }

        public virtual void Stop()
        {
            _componentManager.Stop();
        }
    }
}
