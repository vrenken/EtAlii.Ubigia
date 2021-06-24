// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class StorageScaffolding : IScaffolding
    {
        private readonly IStorageConfiguration _storageConfiguration;

        public StorageScaffolding(IStorageConfiguration storageConfiguration)
        {
            _storageConfiguration = storageConfiguration;
        }

        public void Register(Container container)
        {
            container.Register(() => _storageConfiguration);
            container.Register(() => new SerializerFactory().Create());
        }
    }
}
