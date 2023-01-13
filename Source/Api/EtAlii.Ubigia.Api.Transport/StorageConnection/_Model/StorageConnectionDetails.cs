// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System;
using System.Diagnostics.CodeAnalysis;

public sealed class StorageConnectionDetails : IStorageConnectionDetails
{
    /// <inheritdoc />
    public Uri ManagementAddress { get; private set; }

    /// <inheritdoc />
    public Uri DataAddress { get; private set; }

    public void Update(Storage storage, string managementAddress, string dataAddress)
    {
        Update(storage,new Uri(managementAddress, UriKind.Absolute), new Uri(dataAddress, UriKind.Absolute));
    }

    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S1313:RSPEC-1313 - Using hardcoded IP addresses is security-sensitive",
        Justification = "Safe to do so here.")]
    private void Update(Storage storage, Uri managementAddress, Uri dataAddress)
    {
        // A bit of a dirty patch but the server might not even completely know how the
        // management and data API can be accessed. Let's do some magic to still find the right
        // addresses.
        var managementUriBuilder = new UriBuilder(managementAddress);
        if (managementAddress.Host == "0.0.0.0" || managementAddress.Host == "255.255.255.255")
        {
            managementUriBuilder.Host = new Uri(storage.Address).Host;
        }
        ManagementAddress = managementUriBuilder.Uri;

        var dataUriBuilder = new UriBuilder(dataAddress);
        if (dataAddress.Host == "0.0.0.0" || dataAddress.Host == "255.255.255.255")
        {
            dataUriBuilder.Host = new Uri(storage.Address).Host;
        }
        DataAddress = dataUriBuilder.Uri;
    }
}
