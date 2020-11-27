namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public interface ILogicalPropertiesSet
    {
        PropertyDictionary Get(in Identifier identifier);
        void Store(in Identifier identifier, PropertyDictionary properties);
    }
}