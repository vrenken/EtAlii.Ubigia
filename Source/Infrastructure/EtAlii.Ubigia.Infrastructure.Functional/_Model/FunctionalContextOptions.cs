﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using EtAlii.Ubigia.Infrastructure.Logical;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public class FunctionalContextOptions : IExtensible
{
    /// <summary>
    /// The host configuration root instance for the current application.
    /// </summary>
    /// <remarks>
    /// This is not the same configuration root as used by client API subsystems.
    /// </remarks>
    public IConfigurationRoot ConfigurationRoot { get; }

    /// <summary>
    /// The context that provides access to the logical layer of the codebase.
    /// </summary>
    public ILogicalContext Logical { get; private set; }

    /// <inheritdoc/>
    IExtension[] IExtensible.Extensions { get; set; }

    /// <summary>
    /// An alternative <see cref="ISystemStatusChecker"/>, else null
    /// </summary>
    public ISystemStatusChecker SystemStatusChecker { get; set; }

    /// <summary>
    /// The name of the infrastructure.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The address (schema+host) at which the storage can be found.
    /// </summary>
    public Uri StorageAddress { get; private set; }

    /// <summary>
    /// Returns the details for all of the services provided by the hosted infrastructure.
    /// </summary>
    public ServiceDetails[] ServiceDetails { get; private set; } = Array.Empty<ServiceDetails>();

    public FunctionalContextOptions(IConfigurationRoot configurationRoot)
    {
        ConfigurationRoot = configurationRoot;

        ((IExtensible)this).Extensions = new IExtension[]
        {
            new FunctionalContextExtension(this)
        };
    }

    /// <summary>
    /// Configure the name, storage address and service details that define the infrastructure.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="storageAddress"></param>
    /// <param name="serviceDetails"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public FunctionalContextOptions Use(string name, Uri storageAddress, ServiceDetails[] serviceDetails)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("No name specified", nameof(name));
        }

        StorageAddress = storageAddress ?? throw new ArgumentNullException(nameof(storageAddress));

        if (!serviceDetails.Any())
        {
            throw new InvalidOperationException("No service details specified during infrastructure configuration");
        }

        Name = name;
        ServiceDetails = serviceDetails;
        return this;
    }

    /// <summary>
    /// Configure an alternative <see cref="ISystemStatusChecker"/> to use in the FunctionalContext.
    /// </summary>
    /// <param name="systemStatusChecker"></param>
    /// <returns></returns>
    public FunctionalContextOptions Use(ISystemStatusChecker systemStatusChecker)
    {
        SystemStatusChecker = systemStatusChecker;
        return this;
    }

    /// <summary>
    /// Configure the logical context that should be used by the infrastructure.
    /// </summary>
    /// <param name="logical"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public FunctionalContextOptions Use(ILogicalContext logical)
    {
        Logical = logical ?? throw new ArgumentException("No logical context specified", nameof(logical));
        return this;
    }
}
