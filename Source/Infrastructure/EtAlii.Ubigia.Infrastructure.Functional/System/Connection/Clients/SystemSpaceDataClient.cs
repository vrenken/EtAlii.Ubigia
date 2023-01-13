// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;

internal sealed class SystemSpaceDataClient : SystemStorageClientBase, ISpaceDataClient
{
    private readonly IFunctionalContext _functionalContext;

    public SystemSpaceDataClient(IFunctionalContext functionalContext)
    {
        _functionalContext = functionalContext;
    }

    /// <inheritdoc />
    public Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
    {
        // Improve the space templating functionality by converting initialization to a script based approach.
        // This is where the template functionality should continue.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/95
        var space = new Space
        {
            Name = spaceName,
            AccountId = accountId,
        };

        return _functionalContext.Spaces.Add(space, template);
    }

    /// <inheritdoc />
    public Task Remove(Guid spaceId)
    {
        return _functionalContext.Spaces.Remove(spaceId);
    }

    /// <inheritdoc />
    public Task<Space> Change(Guid spaceId, string spaceName)
    {
        var space = new Space
        {
            Id = spaceId,
            Name = spaceName,
        };

        return _functionalContext.Spaces.Update(spaceId, space);
    }

    /// <inheritdoc />
    public Task<Space> Get(Guid accountId, string spaceName)
    {
        return _functionalContext.Spaces.Get(accountId, spaceName);
    }

    /// <inheritdoc />
    public Task<Space> Get(Guid spaceId)
    {
        return _functionalContext.Spaces.Get(spaceId);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Space> GetAll(Guid accountId)
    {
        return _functionalContext.Spaces.GetAll(accountId);
    }
}
