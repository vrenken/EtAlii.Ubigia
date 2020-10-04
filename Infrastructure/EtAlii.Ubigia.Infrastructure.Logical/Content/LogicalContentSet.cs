namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContentSet : ILogicalContentSet
    {
        private readonly IFabricContext _fabricContext;

        public LogicalContentSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public IReadOnlyContent Get(Identifier identifier)
        {
            return _fabricContext.Content.Get(identifier);
        }

        public IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId)
        {
            return _fabricContext.Content.Get(identifier, contentPartId);
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            _fabricContext.Content.Store(identifier, contentPart);
        }

        public void Store(Identifier identifier, Content content)
        {
            _fabricContext.Content.Store(identifier, content);
        }
    }
}