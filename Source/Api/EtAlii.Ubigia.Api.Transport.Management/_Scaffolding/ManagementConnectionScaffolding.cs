// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using EtAlii.xTechnology.MicroContainer;

    public class ManagementConnectionScaffolding : IScaffolding
    {
        private readonly ManagementConnectionOptions _options;

        public ManagementConnectionScaffolding(ManagementConnectionOptions options)
        {
            var hasTransportProvider = options.TransportProvider != null;
            if (!hasTransportProvider)
            {
                throw new InvalidInfrastructureOperationException("Error creating management connection: No TransportProvider provided.");
            }

            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options.ConfigurationRoot);
            container.Register<IManagementConnection>(() => new ManagementConnection(_options));
        }
    }
}
