﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Ntfs
{
    using System;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.Ubigia.Persistence.Ntfs;
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

            string baseFolder;
            baseFolder = configuration.GetValue<string>(nameof(baseFolder));
            if (baseFolder == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(StorageService)}: {nameof(baseFolder)} not set in service configuration.");
            }

            string name;
            name = configuration.GetValue<string>(nameof(name));
            if (name == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(StorageService)}: {nameof(name)} not set in service configuration.");
            }

            var storageConfiguration = new StorageConfiguration()
                .Use(name)
                .UseNtfsStorage(baseFolder);
            var storage = new StorageFactory().Create(storageConfiguration);

            container.Register(() => storage);

            return container.GetInstance<IStorageService>();
        }
    }
}
