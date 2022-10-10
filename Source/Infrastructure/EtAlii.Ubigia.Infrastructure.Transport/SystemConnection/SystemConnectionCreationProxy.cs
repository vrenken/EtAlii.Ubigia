// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class SystemConnectionCreationProxy : ISystemConnectionCreationProxy
    {
        private IInfrastructure _infrastructure;

        public ISystemConnection Request()
        {
            // This Options.ConfigurationRoot refers to the host configuration root.
            // In order to use it for the system connection it should have the entries needed by the API subsystems.
            var systemConnectionOptions = new SystemConnectionOptions(_infrastructure.Options.ConfigurationRoot)
                .Use(new SystemTransportProvider(_infrastructure))
                .Use(_infrastructure);
            return Factory.Create<ISystemConnection>(systemConnectionOptions);
        }

        public void Initialize(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }
    }
}
