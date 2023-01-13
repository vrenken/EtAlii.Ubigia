// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

public class LoggingRootDataClient : IRootDataClient
{
    private readonly IRootDataClient _client;
    private readonly ILogger _logger = Log.ForContext<IRootDataClient>();

    public LoggingRootDataClient(IRootDataClient client)
    {
        _client = client;
    }

    public async Task Connect(ISpaceConnection spaceConnection)
    {
        await _client.Connect(spaceConnection).ConfigureAwait(false);
    }

    public async Task Disconnect()
    {
        await _client.Disconnect().ConfigureAwait(false);
    }

    public async Task<Root> Add(string name, RootType rootType)
    {
        _logger.Debug("Adding root (Name: {RootName})", name);
        var start = Environment.TickCount;

        var root = await _client
            .Add(name, rootType)
            .ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Added root (Name: {RootName} Duration: {Duration}ms)", name, duration);

        return root;
    }

    public async Task Remove(Guid id)
    {
        _logger.Debug("Removing root (Id: {RootId})", id);
        var start = Environment.TickCount;

        await _client.Remove(id).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Removed root (Id: {RootId} Duration: {Duration}ms)", id, duration);
    }

    public async Task<Root> Change(Guid rootId, string rootName, RootType rootType)
    {
        _logger.Debug("Changing root (Id: {RootId} Name: {RootName})", rootId, rootName);
        var start = Environment.TickCount;

        var root = await _client.Change(rootId, rootName, rootType).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Changed root (Id: {RootId} Name: {RootName} Duration: {Duration}ms)", rootId, rootName, duration);

        return root;
    }

    public async Task<Root> Get(string rootName)
    {
        _logger.Debug("Getting root (Name: {RootName})", rootName);
        var start = Environment.TickCount;

        var root = await _client.Get(rootName).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Got root (Name: {RootName} Duration: {}ms)", rootName, duration);

        return root;
    }

    public async Task<Root> Get(Guid rootId)
    {
        _logger.Debug("Getting root (Id: {RootId})", rootId);
        var start = Environment.TickCount;

        var root = await _client.Get(rootId).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Got root (Id: {RootId} Duration: {Duration}ms)", rootId, duration);

        return root;
    }

    public async IAsyncEnumerable<Root> GetAll()
    {
        _logger.Debug("Getting all roots");
        var start = Environment.TickCount;

        var result = _client
            .GetAll()
            .ConfigureAwait(false);
        await foreach (var item in result)
        {
            yield return item;
        }

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Got all roots (Duration: {Duration}ms)", duration);
    }
}
