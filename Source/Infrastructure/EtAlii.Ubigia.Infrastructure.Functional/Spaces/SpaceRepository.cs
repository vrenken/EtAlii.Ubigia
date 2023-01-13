// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class SpaceRepository : ISpaceRepository
{
    private readonly ILogicalContext _logicalContext;
    private readonly ISpaceInitializer _spaceInitializer;

    public SpaceRepository(
        ILogicalContext logicalContext,
        ISpaceInitializer spaceInitializer)
    {
        _spaceInitializer = spaceInitializer;
        _logicalContext = logicalContext;
    }

    /// <inheritdoc />
    public async Task<Space> Add(Space item, SpaceTemplate template)
    {
        var (addedSpace, isAdded) = await _logicalContext.Spaces
            .Add(item, template)
            .ConfigureAwait(false);
        if (isAdded)
        {
            await _spaceInitializer
                .Initialize(addedSpace, template)
                .ConfigureAwait(false);
        }

        return addedSpace;
    }

    /// <inheritdoc />
    public Task<Space> Get(Guid accountId, string spaceName)
    {
        return _logicalContext.Spaces.Get(accountId, spaceName);
    }

    /// <inheritdoc />
    public Task<Space> Get(Guid itemId)
    {
        return _logicalContext.Spaces.Get(itemId);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Space> GetAll()
    {
        return _logicalContext.Spaces.GetAll();
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Space> GetAll(Guid accountId)
    {
        return _logicalContext.Spaces.GetAll(accountId);
    }

    /// <inheritdoc />
    public Task<Space> Update(Guid itemId, Space item)
    {
        return _logicalContext.Spaces.Update(itemId, item);
    }

    /// <inheritdoc />
    public Task Remove(Guid itemId)
    {
        return _logicalContext.Spaces.Remove(itemId);
    }

    /// <inheritdoc />
    public Task Remove(Space item)
    {
        return _logicalContext.Spaces.Remove(item);
    }
}
