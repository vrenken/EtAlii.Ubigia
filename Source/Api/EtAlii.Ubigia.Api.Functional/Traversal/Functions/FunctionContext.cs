// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public class FunctionContext : IFunctionContext
{
    IPathProcessor IFunctionContext.PathProcessor => _pathProcessor;
    private readonly IPathProcessor _pathProcessor;

    IItemToIdentifierConverter IFunctionContext.ItemToIdentifierConverter => _itemToIdentifierConverter;
    private readonly IItemToIdentifierConverter _itemToIdentifierConverter;

    public FunctionContext(
        IPathProcessor pathProcessor,
        IItemToIdentifierConverter itemToIdentifierConverter)
    {
        _pathProcessor = pathProcessor;
        _itemToIdentifierConverter = itemToIdentifierConverter;
    }
}
