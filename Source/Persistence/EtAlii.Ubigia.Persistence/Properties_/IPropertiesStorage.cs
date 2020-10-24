namespace EtAlii.Ubigia.Persistence
{
    public interface IPropertiesStorage
    {
        void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "_Default");
        PropertyDictionary Retrieve(ContainerIdentifier container, string name = "_Default");
    }
}
