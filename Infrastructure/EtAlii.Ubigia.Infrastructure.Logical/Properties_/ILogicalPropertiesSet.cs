namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public interface ILogicalPropertiesSet
    {
        PropertyDictionary Get(Identifier identifier);
        void Store(Identifier identifier, PropertyDictionary properties);
    }
}