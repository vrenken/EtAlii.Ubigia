namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IPropertiesStorer
    {
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}