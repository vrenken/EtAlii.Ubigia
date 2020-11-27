namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IPropertiesRepository
    {
        void Store(in Identifier identifier, PropertyDictionary properties);
        PropertyDictionary Get(in Identifier identifier);
    }
}