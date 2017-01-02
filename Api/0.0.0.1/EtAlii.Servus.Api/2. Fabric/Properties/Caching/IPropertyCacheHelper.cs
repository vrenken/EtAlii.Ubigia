namespace EtAlii.Servus.Api.Fabric
{
    public interface IPropertyCacheHelper
    {
        PropertyDictionary GetProperties(Identifier identifier);
        void StoreProperties(Identifier identifier, PropertyDictionary properties);
    }
}