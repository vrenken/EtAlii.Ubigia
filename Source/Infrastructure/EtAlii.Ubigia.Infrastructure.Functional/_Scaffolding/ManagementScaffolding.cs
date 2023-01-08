// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using EtAlii.Ubigia.Infrastructure.Logical;
using EtAlii.xTechnology.MicroContainer;

internal class ManagementScaffolding : IScaffolding
{
    private readonly FunctionalContextOptions _options;

    public ManagementScaffolding(FunctionalContextOptions options)
    {
        _options = options;
    }
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IStorageInitializer, StorageInitializer>();
        container.Register<ILocalStorageInitializer, LocalStorageInitializer>();

        container.Register<IStorageRepository>(services =>
        {
            var logicalContext = services.GetInstance<ILogicalContext>();
            var localStorageInitializer = services.GetInstance<ILocalStorageInitializer>();
            var storageInitializer = services.GetInstance<IStorageInitializer>();
            var localStorageGetter = services.GetInstance<ILocalStorageGetter>();
            return new StorageRepository(_options, logicalContext, localStorageInitializer, storageInitializer, localStorageGetter);
        });
        container.Register<IAccountRepository, AccountRepository>();
        container.Register<ISpaceRepository, SpaceRepository>();
    }
}
