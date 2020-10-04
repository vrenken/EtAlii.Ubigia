namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IPropertiesSet
    {
        PropertyDictionary Get(Identifier identifier);
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}