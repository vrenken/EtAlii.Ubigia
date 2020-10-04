﻿namespace EtAlii.Ubigia.Persistence
{
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
