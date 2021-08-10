// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using EtAlii.xTechnology.MicroContainer;

    public class ManagementConnectionScaffolding : IScaffolding
    {
        private readonly IManagementConnectionOptions _options;

        public ManagementConnectionScaffolding(IManagementConnectionOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);
            container.Register<IManagementConnection, ManagementConnection>();
        }
    }
}
