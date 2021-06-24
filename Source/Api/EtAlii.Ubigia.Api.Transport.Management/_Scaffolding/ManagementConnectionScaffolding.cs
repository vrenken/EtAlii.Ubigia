// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using EtAlii.xTechnology.MicroContainer;

    public class ManagementConnectionScaffolding : IScaffolding
    {
        private readonly IManagementConnectionConfiguration _configuration;

        public ManagementConnectionScaffolding(IManagementConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register<IManagementConnection, ManagementConnection>();
        }
    }
}
