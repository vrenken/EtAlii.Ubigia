// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public static class StorageConnectionUseExtensions
    {
        public static TStorageConnectionConfiguration Use<TStorageConnectionConfiguration>(this TStorageConnectionConfiguration configuration,IStorageTransport transport)
            where TStorageConnectionConfiguration : StorageConnectionConfiguration 
        {

            var editableConfiguration = (IEditableStorageConnectionConfiguration) configuration;
            
            if (editableConfiguration.Transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this StorageConnectionConfiguration");
            }

            editableConfiguration.Transport = transport ?? throw new ArgumentException("No transport specified", nameof(transport));
            return configuration;
        }
    }
}