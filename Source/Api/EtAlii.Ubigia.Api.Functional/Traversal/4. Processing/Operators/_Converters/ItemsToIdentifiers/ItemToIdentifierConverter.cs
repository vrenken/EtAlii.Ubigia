// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    internal class ItemToIdentifierConverter : IItemToIdentifierConverter
    {
        public Identifier Convert(object item)
        {
            switch (item)
            {
                case Identifier identifier:
                    return identifier;
                case IReadOnlyEntry entry:
                    return entry.Id;
                case INode node:
                    return node.Id;
                default:
                    throw new ScriptProcessingException($"The {GetType().Name} is unable to convert the specified object: {item ?? "NULL"}");
            }

        }
    }
}
