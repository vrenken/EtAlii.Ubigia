namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IPropertiesRetriever
    {
        PropertyDictionary Retrieve(ContainerIdentifier container, string name);
    }
}
