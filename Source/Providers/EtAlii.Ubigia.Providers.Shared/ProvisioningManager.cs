namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class ProvisioningManager : IProvisioningManager
    {
        public IGraphSLScriptContext Data { get; }

        private readonly IProviderManager _providerManager;

        /// <summary>
        /// The Configuration used to instantiate this Context.
        /// </summary>
        public IProvisioningConfiguration Configuration { get; }

        public string Status => _providerManager.Status;

        protected ProvisioningManager(
            IGraphSLScriptContext data,
            IProvisioningConfiguration configuration,
            IProviderManager providerManager)
        {
            Data = data;
            Configuration = configuration;
            _providerManager = providerManager;
        }

        public virtual Task Start()
        {
            return _providerManager.Start();
        }

        public virtual Task Stop()
        {
            return _providerManager.Stop();
        }
    }
}
