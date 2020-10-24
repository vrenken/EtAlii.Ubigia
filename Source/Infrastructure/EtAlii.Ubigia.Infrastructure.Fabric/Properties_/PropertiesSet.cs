namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public class PropertiesSet : IPropertiesSet
    {
        private readonly IPropertiesGetter _propertiesGetter;
        private readonly IPropertiesStorer _propertiesStorer;

        public PropertiesSet(
            IPropertiesGetter propertiesGetter, 
            IPropertiesStorer propertiesStorer)
        {
            _propertiesGetter = propertiesGetter;
            _propertiesStorer = propertiesStorer;
        }

        public PropertyDictionary Get(Identifier identifier)
        {
            return _propertiesGetter.Get(identifier);
        }

        public void Store(Identifier identifier, PropertyDictionary properties)
        {
            _propertiesStorer.Store(identifier, properties);
        }
    }
}