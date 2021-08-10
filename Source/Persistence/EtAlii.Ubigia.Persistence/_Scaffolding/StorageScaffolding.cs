// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class StorageScaffolding : IScaffolding
    {
        private readonly IStorageOptions _options;

        public StorageScaffolding(IStorageOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);
            container.Register(() => new SerializerFactory().Create());
        }
    }
}
