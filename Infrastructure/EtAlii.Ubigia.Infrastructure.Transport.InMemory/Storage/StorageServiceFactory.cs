﻿namespace EtAlii.Ubigia.Infrastructure.Transport.InMemory
{
    using System;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.Ubigia.Persistence.InMemory;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class StorageServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register(() => configuration);
            container.Register<IStorageService, StorageService>();

            string name;
            name = configuration.GetValue<string>(nameof(name));
            if (name == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(StorageService)}: {nameof(name)} not set in service configuration.");
            }

            var storageConfiguration = new StorageConfiguration()
                .Use(name)
				.UseInMemoryStorage()
                .Use(UbigiaDiagnostics.DefaultConfiguration);
            var storage = new StorageFactory().Create(storageConfiguration);

            container.Register(() => storage);

            return container.GetInstance<IStorageService>();
        }
    }
}
