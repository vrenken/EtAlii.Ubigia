// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    public class StorageConnectionConfiguration : ConfigurationBase, IStorageConnectionConfiguration, IEditableStorageConnectionConfiguration
    {
        IStorageTransport IEditableStorageConnectionConfiguration.Transport { get => Transport; set => Transport = value; }
        public IStorageTransport Transport { get; private set; }
    }
}