namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IPropertiesSet
    {
        PropertyDictionary Get(Identifier identifier);
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}