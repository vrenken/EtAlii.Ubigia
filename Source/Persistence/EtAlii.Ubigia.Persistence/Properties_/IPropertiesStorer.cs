namespace EtAlii.Ubigia.Persistence
{
    public interface IPropertiesStorer
    {
        void Store(ContainerIdentifier container, PropertyDictionary properties, string name);
    }
}
