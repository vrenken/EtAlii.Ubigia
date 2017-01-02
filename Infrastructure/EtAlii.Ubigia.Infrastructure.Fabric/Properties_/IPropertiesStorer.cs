namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IPropertiesStorer
    {
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}