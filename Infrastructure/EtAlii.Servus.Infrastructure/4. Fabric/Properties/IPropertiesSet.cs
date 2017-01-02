namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IPropertiesSet
    {
        PropertyDictionary Get(Identifier identifier);
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}