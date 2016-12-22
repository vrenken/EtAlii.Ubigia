namespace EtAlii.Servus.Infrastructure.Logical
{
    using EtAlii.Servus.Api;

    public interface ILogicalPropertiesSet
    {
        PropertyDictionary Get(Identifier identifier);
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}