namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IPropertiesGetter
    {
        PropertyDictionary Get(in Identifier identifier);
    }
}