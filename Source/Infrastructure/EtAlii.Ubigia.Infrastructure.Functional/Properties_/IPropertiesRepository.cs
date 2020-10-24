namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IPropertiesRepository
    {
        void Store(Identifier identifier, PropertyDictionary properties);
        PropertyDictionary Get(Identifier identifier);
    }
}