namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IPropertiesGetter
    {
        PropertyDictionary Get(Identifier identifier);
    }
}