namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalPropertiesSet : ILogicalPropertiesSet
    {
        private readonly IFabricContext _fabricContext;

        public LogicalPropertiesSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public PropertyDictionary Get(Identifier identifier)
        {
            return _fabricContext.Properties.Get(identifier);
        }

        public void Store(Identifier identifier, PropertyDictionary properties)
        {
            _fabricContext.Properties.Store(identifier, properties);
        }
    }
}