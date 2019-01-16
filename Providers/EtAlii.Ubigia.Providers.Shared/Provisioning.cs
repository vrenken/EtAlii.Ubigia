namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional;

    public class Provisioning : IProvisioning
    {
        public IGraphSLScriptContext Data { get; }

        private readonly IProviderManager _providerManager;

        public IProvisioningConfiguration Configuration { get; }

        public string Status => _providerManager.Status;

        protected Provisioning(
            IGraphSLScriptContext data,
            IProvisioningConfiguration configuration,
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
