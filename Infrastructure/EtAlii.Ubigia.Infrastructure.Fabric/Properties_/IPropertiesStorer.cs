namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IPropertiesStorer
    {
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}