// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;

public class ServiceDetails
{
    /// <summary>
    /// The name of the service to which the details relate.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The address of the service management API.
    /// </summary>
    public Uri ManagementAddress { get; }

    /// <summary>
    /// The address of the service data API.
    /// </summary>
    public Uri DataAddress { get; }

    /// <summary>
    /// The flat (scheme+host) address at which the storage can be found.
    /// </summary>
    public Uri StorageAddress { get; }

    public ServiceDetails(string name, Uri managementAddress, Uri dataAddress, Uri storageAddress)
    {
        Name = name;
        ManagementAddress = managementAddress;
        DataAddress = dataAddress;
        StorageAddress = storageAddress;
    }
}
