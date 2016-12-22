namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IPropertiesRetriever
    {
        PropertyDictionary Retrieve(ContainerIdentifier container, string name);
    }
}
