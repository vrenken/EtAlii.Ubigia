namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Api;

    public interface IPropertiesRepository
    {
        void Store(Identifier identifier, PropertyDictionary properties);
        PropertyDictionary Get(Identifier identifier);
    }
}