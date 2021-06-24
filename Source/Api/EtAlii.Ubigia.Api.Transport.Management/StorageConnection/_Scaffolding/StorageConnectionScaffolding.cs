// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class StorageConnectionScaffolding : IScaffolding
    {
        private readonly IStorageConnectionConfiguration _configuration;

        public StorageConnectionScaffolding(IStorageConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration.Transport);
            container.Register(() => _configuration);

            container.Register<IContextCorrelator, ContextCorrelator>();
            container.Register<IAuthenticationManagementContext, AuthenticationManagementContext>();
            container.Register<IInformationContext, InformationContext>();
            container.Register<IStorageContext, StorageContext>();
            container.Register<IAccountContext, AccountContext>();
            container.Register<ISpaceContext, SpaceContext>();
        }
    }
}
