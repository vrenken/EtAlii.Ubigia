// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics;

using System;
using System.Threading.Tasks;

//using EtAlii.xTechnology.Logging

public sealed class ProfilingManagementConnection : IManagementConnection
{
    private readonly IManagementConnection _decoree;
    //private readonly IProfiler _profiler

    public ProfilingManagementConnection(
        IManagementConnection decoree
        //IProfiler profiler
    )
    {
        _decoree = decoree;
        //_profiler = profiler
    }

    /// <inheritdoc />
    public Storage Storage => _decoree.Storage;

    /// <inheritdoc />
    public IStorageContext Storages => _decoree.Storages;

    /// <inheritdoc />
    public IAccountContext Accounts => _decoree.Accounts;

    /// <inheritdoc />
    public ISpaceContext Spaces => _decoree.Spaces;

    /// <inheritdoc />
    public bool IsConnected => _decoree.IsConnected;

    /// <inheritdoc />
    public IStorageConnectionDetails Details => _decoree.Details;

    /// <inheritdoc />
    public ManagementConnectionOptions Options => _decoree.Options;

    /// <inheritdoc />
    public async Task Open()
    {
        await _decoree.Open().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
    {
        return await _decoree.OpenSpace(accountId, spaceId).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDataConnection> OpenSpace(Space space)
    {
        return await _decoree.OpenSpace(space).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
    {
        return await _decoree.OpenSpace(accountName,spaceName).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task Close()
    {
        await _decoree.Close().ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await _decoree
            .DisposeAsync()
            .ConfigureAwait(false);
    }
}
