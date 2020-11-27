namespace EtAlii.Ubigia.Api.Fabric
{
    public interface IPropertyCacheHelper
    {
        PropertyDictionary GetProperties(in Identifier identifier);
        void StoreProperties(in Identifier identifier, PropertyDictionary properties);
    }
}