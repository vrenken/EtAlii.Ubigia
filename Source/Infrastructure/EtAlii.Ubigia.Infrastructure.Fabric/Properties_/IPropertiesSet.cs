namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IPropertiesSet
    {
        PropertyDictionary Get(in Identifier identifier);
        void Store(in Identifier identifier, PropertyDictionary properties);
    }
}