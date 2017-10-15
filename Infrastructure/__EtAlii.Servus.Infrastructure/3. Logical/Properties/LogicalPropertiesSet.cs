namespace EtAlii.Servus.Infrastructure.Logical
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Fabric;

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