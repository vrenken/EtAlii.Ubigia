// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public static class StorageConnectionOptionsUseExtensions
    {
        public static TStorageConnectionOptions Use<TStorageConnectionOptions>(this TStorageConnectionOptions options, IStorageTransport transport)
            where TStorageConnectionOptions : StorageConnectionOptions
        {

            var editableOptions = (IEditableStorageConnectionOptions) options;

            if (editableOptions.Transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this StorageConnectionOptions");
            }

            editableOptions.Transport = transport ?? throw new ArgumentException("No transport specified", nameof(transport));
            return options;
        }
    }
}
