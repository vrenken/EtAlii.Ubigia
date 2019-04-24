﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

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
                    throw new ScriptProcessingException($"The {this.GetType().Name} is unable to convert the specified object: {item ?? "NULL"}");
            }                    

        }
    }
}
