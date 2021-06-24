// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public class ContentDefinitionSet : IContentDefinitionSet
    {
        private readonly IContentDefinitionGetter _contentDefinitionGetter;
        private readonly IContentDefinitionPartGetter _contentDefinitionPartGetter;
        private readonly IContentDefinitionStorer _contentDefinitionStorer;
        private readonly IContentDefinitionPartStorer _contentDefinitionPartStorer;

        public ContentDefinitionSet(
            IContentDefinitionGetter contentDefinitionGetter, 
            IContentDefinitionPartGetter contentDefinitionPartGetter, 
            IContentDefinitionStorer contentDefinitionStorer, 
            IContentDefinitionPartStorer contentDefinitionPartStorer)
        {
            _contentDefinitionGetter = contentDefinitionGetter;
            _contentDefinitionPartGetter = contentDefinitionPartGetter;
            _contentDefinitionStorer = contentDefinitionStorer;
            _contentDefinitionPartStorer = contentDefinitionPartStorer;
        }


        public Task<ContentDefinition> Get(Identifier identifier)
        {
            return _contentDefinitionGetter.Get(identifier);
        }

        public Task<ContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId)
        {
            return _contentDefinitionPartGetter.Get(identifier, contentDefinitionPartId);
        }

        public void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            _contentDefinitionPartStorer.Store(identifier, contentDefinitionPart);
        }

        public void Store(in Identifier identifier, ContentDefinition contentDefinition)
        {
            _contentDefinitionStorer.Store(identifier, contentDefinition);
        }
    }
}