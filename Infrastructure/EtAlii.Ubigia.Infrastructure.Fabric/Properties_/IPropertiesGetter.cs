namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IPropertiesGetter
    {
        PropertyDictionary Get(Identifier identifier);
    }
}