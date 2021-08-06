// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using Microsoft.Extensions.Configuration;

    public class StorageConnectionOptions : ConfigurationBase, IStorageConnectionOptions, IEditableStorageConnectionOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc />
        IStorageTransport IEditableStorageConnectionOptions.Transport { get => Transport; set => Transport = value; }

        /// <inheritdoc />
        public IStorageTransport Transport { get; private set; }

        public StorageConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }
    }
}
