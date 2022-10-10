// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal class CommonSystemConnectionExtension : IExtension
    {
        private readonly SystemConnectionOptions _options;

        public CommonSystemConnectionExtension(SystemConnectionOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var serviceDetails = _options.Infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

            var transport = _options.TransportProvider.GetStorageTransport(serviceDetails.ManagementAddress);
            var scaffoldings = transport
                .CreateScaffolding()
                .Concat(new IScaffolding[]
                {
                    new SystemConnectionScaffolding(_options),
                    new SystemInfrastructureScaffolding(),
                });

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
