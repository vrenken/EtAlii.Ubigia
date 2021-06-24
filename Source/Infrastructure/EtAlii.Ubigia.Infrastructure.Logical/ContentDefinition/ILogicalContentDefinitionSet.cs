// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;

    public interface ILogicalContentDefinitionSet
    {
        Task<ContentDefinition> Get(Identifier identifier);
        Task<ContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId);
        void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        void Store(in Identifier identifier, ContentDefinition contentDefinition);
    }
}