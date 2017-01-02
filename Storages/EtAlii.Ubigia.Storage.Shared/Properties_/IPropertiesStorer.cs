namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IPropertiesStorer
    {
        void Store(ContainerIdentifier container, PropertyDictionary properties, string name);
    }
}
