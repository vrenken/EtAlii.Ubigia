// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using System;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

public sealed class ManagementConnectionOptions : IExtensible
{
    /// <summary>
    /// The client configuration root that will be used to configure the management connection.
    /// </summary>
    public IConfigurationRoot ConfigurationRoot { get; }

    /// <inheritdoc/>
    IExtension[] IExtensible.Extensions { get; set; }

    /// <summary>
    /// The storage transport provider used to create the transport layer components of this management connection.
    /// </summary>
    public IStorageTransportProvider TransportProvider { get; private set; }

    /// <summary>
    /// The factory extension method that is used to extend the connection creation and instantiation.
    /// One example is a popup dialog that allows for (re)configuration of the credentials/addresses etc.
    /// </summary>
    public Func<IManagementConnection> FactoryExtension { get; private set; }

    /// <summary>
    /// The address to which this management connection should connect.
    /// </summary>
    public Uri Address { get; private set; }

    /// <summary>
    /// The account name to be used when connecting.
    /// </summary>
    public string AccountName { get; private set; }

    /// <summary>
    /// The account password to be used when connecting.
    /// </summary>
    public string Password { get; private set; }

    public ManagementConnectionOptions(IConfigurationRoot configurationRoot)
    {
        ConfigurationRoot = configurationRoot;
        ((IExtensible)this).Extensions = new IExtension[]
        {
            new CommonManagementConnectionExtension(this)
        };
    }

    /// <summary>
    /// Configures the factory extension method that is used to extend the connection creation and instantiation.
    /// </summary>
    public ManagementConnectionOptions Use(Func<IManagementConnection> factoryExtension)
    {
        FactoryExtension = factoryExtension;
        return this;
    }

    /// <summary>
    /// Configures the storage transport provider that should be used to provide the transport layer components of this management connection.
    /// </summary>
    public ManagementConnectionOptions Use(IStorageTransportProvider transportProvider)
    {
        if (TransportProvider != null)
        {
            throw new ArgumentException("A TransportProvider has already been assigned to this ManagementConnectionOptions", nameof(transportProvider));
        }

        TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

        return this;
    }

    /// <summary>
    /// Configures the address that should be used when connecting.
    /// </summary>
    public ManagementConnectionOptions Use(Uri address)
    {
        if (Address != null)
        {
            throw new InvalidOperationException("An address has already been assigned to this ManagementConnectionOptions");
        }

        Address = address ?? throw new ArgumentNullException(nameof(address));
        return this;
    }

    /// <summary>
    /// Configures both the account name and account password that should be used when connecting.
    /// </summary>
    public ManagementConnectionOptions Use(string accountName, string password)
    {
        if (string.IsNullOrWhiteSpace(accountName))
        {
            throw new ArgumentException("No account name specified: A management connection cannot be made without account identification", nameof(accountName));
        }
        if (AccountName != null)
        {
            throw new InvalidOperationException("An account name has already been assigned to this ManagementConnectionOptions");
        }
        if (Password != null)
        {
            throw new InvalidOperationException("A password has already been assigned to this ManagementConnectionOptions");
        }

        AccountName = accountName;
        Password = password;
        return this;
    }
}
