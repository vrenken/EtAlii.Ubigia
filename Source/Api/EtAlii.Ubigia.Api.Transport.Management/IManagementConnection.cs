﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using System;
using System.Threading.Tasks;

public interface IManagementConnection : IAsyncDisposable
{
    /// <summary>
    /// Returns true when the management connection has been established.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// The Options used to instantiate this ManagementConnection.
    /// </summary>
    ManagementConnectionOptions Options { get; }

    /// <summary>
    /// Additional details about the storage connection.
    /// </summary>
    IStorageConnectionDetails Details { get; }

    /// <summary>
    /// The storage to which the connection talks.
    /// </summary>
    Storage Storage { get; }

    /// <summary>
    /// A context through which to call storage related RPC's.
    /// </summary>
    IStorageContext Storages { get; }

    /// <summary>
    /// A context through which to call account related RPC's.
    /// </summary>
    IAccountContext Accounts { get; }

    /// <summary>
    /// A context through which to call space related RPC's.
    /// </summary>
    ISpaceContext Spaces { get; }

    Task<IDataConnection> OpenSpace(Space space);
    Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId);
    Task<IDataConnection> OpenSpace(string accountName, string spaceName);

    Task Open();
    Task Close();
}
