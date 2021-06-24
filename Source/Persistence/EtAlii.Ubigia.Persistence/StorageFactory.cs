// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.MicroContainer;

    public class StorageFactory : IStorageFactory
    {
        public IStorage Create()
        {
            throw new NotSupportedException();
        }

        public IStorage Create(IStorageConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new NotSupportedException("The name is required to construct a Storage instance");
            }

            var container = new Container();

            var scaffoldings = new List<IScaffolding>(new IScaffolding[]
            {
                new StorageScaffolding(configuration),
                new ComponentsScaffolding(),
                new BlobsScaffolding(),
                new ContainersScaffolding(),
                new ItemsScaffolding(),
                new PropertiesScaffolding(),
                new ImmutablesScaffolding(),
            });

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return configuration.GetStorage(container);
        }
    }
}
