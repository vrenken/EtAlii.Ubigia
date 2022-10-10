// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ManagementScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IStorageInitializer, StorageInitializer>();
            container.Register<ILocalStorageInitializer, LocalStorageInitializer>();

            container.Register<IStorageRepository, StorageRepository>();
            container.Register<IAccountRepository, AccountRepository>();
            container.Register<ISpaceRepository, SpaceRepository>();
        }
    }
}
