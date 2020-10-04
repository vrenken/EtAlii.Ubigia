namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContentDefinitionSet : ILogicalContentDefinitionSet
    {
        private readonly IFabricContext _fabricContext;

        public LogicalContentDefinitionSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public IReadOnlyContentDefinition Get(Identifier identifier)
        {
            return _fabricContext.ContentDefinition.Get(identifier);
        }

        public IReadOnlyContentDefinitionPart Get(Identifier identifier, ulong contentDefinitionPartId)
        {
            return _fabricContext.ContentDefinition.Get(identifier, contentDefinitionPartId);
        }

        public void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            _fabricContext.ContentDefinition.Store(identifier, contentDefinitionPart);
        }

        public void Store(Identifier identifier, ContentDefinition contentDefinition)
        {
            _fabricContext.ContentDefinition.Store(identifier, contentDefinition);
        }
    }
}