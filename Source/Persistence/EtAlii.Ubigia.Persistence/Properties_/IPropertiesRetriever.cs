namespace EtAlii.Ubigia.Persistence
{
    public interface IPropertiesRetriever
    {
        PropertyDictionary Retrieve(ContainerIdentifier container, string name);
    }
}
