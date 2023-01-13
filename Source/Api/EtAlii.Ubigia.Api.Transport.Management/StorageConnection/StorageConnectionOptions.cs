// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using System;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public class StorageConnectionOptions : IStorageConnectionOptions, IEditableStorageConnectionOptions
{
    /// <inheritdoc />
    public IConfigurationRoot ConfigurationRoot { get; }

    /// <inheritdoc/>
    IExtension[] IExtensible.Extensions { get; set; }

    /// <inheritdoc />
    IStorageTransport IEditableStorageConnectionOptions.Transport { get => Transport; set => Transport = value; }

    /// <inheritdoc />
    public IStorageTransport Transport { get; private set; }

    public StorageConnectionOptions(IConfigurationRoot configurationRoot)
    {
        ConfigurationRoot = configurationRoot;
        ((IExtensible)this).Extensions = Array.Empty<IExtension>();
    }
}
