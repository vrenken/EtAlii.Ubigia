// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric;

public class LogicalContentSet : ILogicalContentSet
{
    private readonly IFabricContext _fabricContext;

    public LogicalContentSet(IFabricContext fabricContext)
    {
        _fabricContext = fabricContext;
    }

    /// <inheritdoc />
    public Task<Content> Get(Identifier identifier)
    {
        return _fabricContext.Content.Get(identifier);
    }

    /// <inheritdoc />
    public Task<ContentPart> Get(Identifier identifier, ulong contentPartId)
    {
        return _fabricContext.Content.Get(identifier, contentPartId);
    }

    /// <inheritdoc />
    public Task Store(in Identifier identifier, ContentPart contentPart)
    {
        return _fabricContext.Content.Store(identifier, contentPart);
    }

    /// <inheritdoc />
    public Task Store(in Identifier identifier, Content content)
    {
        return _fabricContext.Content.Store(identifier, content);
    }
}
