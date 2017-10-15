namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IPropertiesGetter
    {
        PropertyDictionary Get(Identifier identifier);
    }
}