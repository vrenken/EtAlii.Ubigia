﻿namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
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
