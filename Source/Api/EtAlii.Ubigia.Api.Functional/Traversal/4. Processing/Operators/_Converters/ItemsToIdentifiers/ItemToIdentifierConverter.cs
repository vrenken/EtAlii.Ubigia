// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.Ubigia.Api.Logical;

internal class ItemToIdentifierConverter : IItemToIdentifierConverter
{
    public Identifier Convert(object item)
    {
        return item switch
        {
            Identifier identifier => identifier,
            IReadOnlyEntry entry => entry.Id,
            Node node => node.Id,
            _ => throw new ScriptProcessingException($"The {GetType().Name} is unable to convert the specified object: {item ?? "NULL"}")
        };
    }
}
