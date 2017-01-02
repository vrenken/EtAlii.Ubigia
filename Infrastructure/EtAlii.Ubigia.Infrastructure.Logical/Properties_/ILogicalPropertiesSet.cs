namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.Ubigia.Api;

    public interface ILogicalPropertiesSet
    {
        PropertyDictionary Get(Identifier identifier);
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}