namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IPropertiesStorer
    {
        void Store(in Identifier identifier, PropertyDictionary properties);
    }
}