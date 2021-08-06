// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class StorageOptions : IStorageOptions
    {
        public IConfigurationRoot ConfigurationRoot { get; }

        public IStorageExtension[] Extensions { get; private set; }

        public string Name { get; private set; }


        private Func<Container, IStorage> _getStorage;
        public IStorage GetStorage(Container container)
        {
            return _getStorage(container);
        }

        public StorageOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            Extensions = Array.Empty<IStorageExtension>();
        }

        public IStorageOptions Use(IStorageExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("No extensions specified", nameof(extensions));
            }

            var alreadyRegistered = Extensions.FirstOrDefault(e => extensions.Any(e2 => e2.GetType() == e.GetType()));
            if(alreadyRegistered != null)
            {
                throw new InvalidOperationException("Extension already registered: " + alreadyRegistered.GetType().Name);
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IStorageOptions Use(string name)
        {
            Name = name;
            _getStorage = container =>
            {
                container.Register<IStorage, DefaultStorage>();
                return container.GetInstance<IStorage>();
            };
            return this;
        }


        public IStorageOptions Use<TStorage>()
            where TStorage : class, IStorage
        {
            _getStorage = container =>
            {
                container.Register<IStorage, TStorage>();
                return container.GetInstance<IStorage>();
            };

            return this;
        }

    }
}
