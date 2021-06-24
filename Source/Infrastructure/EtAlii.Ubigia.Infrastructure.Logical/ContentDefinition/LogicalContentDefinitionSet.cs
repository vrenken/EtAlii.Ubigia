// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContentDefinitionSet : ILogicalContentDefinitionSet
    {
        private readonly IFabricContext _fabricContext;

        public LogicalContentDefinitionSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public Task<ContentDefinition> Get(Identifier identifier)
        {
            return _fabricContext.ContentDefinition.Get(identifier);
        }

        public Task<ContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId)
        {
            return _fabricContext.ContentDefinition.Get(identifier, contentDefinitionPartId);
        }

        public void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            _fabricContext.ContentDefinition.Store(identifier, contentDefinitionPart);
        }

        public void Store(in Identifier identifier, ContentDefinition contentDefinition)
        {
            _fabricContext.ContentDefinition.Store(identifier, contentDefinition);
        }
    }
}