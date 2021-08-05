﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Ntfs
{
    using System;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.Ubigia.Persistence.Ntfs;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class StorageServiceFactory : ServiceFactoryBase
    {
        public override IService Create(
            IConfigurationSection configuration,
            IConfiguration configurationRoot,
            IConfigurationDetails configurationDetails)
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

            var storageOptions = new StorageOptions(configurationRoot)
                .Use(name)
                .UseNtfsStorage(baseFolder)
                .UseStorageDiagnostics();
            var storage = new StorageFactory().Create(storageOptions);

            container.Register(() => storage);

            return container.GetInstance<IStorageService>();
        }
    }
}
