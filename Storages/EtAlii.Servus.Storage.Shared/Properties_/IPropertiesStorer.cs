namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IPropertiesStorer
    {
        void Store(ContainerIdentifier container, PropertyDictionary properties, string name);
    }
}
