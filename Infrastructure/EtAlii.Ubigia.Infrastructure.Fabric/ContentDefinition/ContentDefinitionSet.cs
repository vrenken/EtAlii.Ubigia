namespace EtAlii.Ubigia.Infrastructure.Fabric
{
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


        public IReadOnlyContentDefinition Get(Identifier identifier)
        {
            return _contentDefinitionGetter.Get(identifier);
        }

        public IReadOnlyContentDefinitionPart Get(Identifier identifier, ulong contentDefinitionPartId)
        {
            return _contentDefinitionPartGetter.Get(identifier, contentDefinitionPartId);
        }

        public void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            _contentDefinitionPartStorer.Store(identifier, contentDefinitionPart);
        }

        public void Store(Identifier identifier, ContentDefinition contentDefinition)
        {
            _contentDefinitionStorer.Store(identifier, contentDefinition);
        }
    }
}