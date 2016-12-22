namespace EtAlii.Servus.Storage
{
    using System;
    using EtAlii.Servus.Api;

    public interface IPropertiesStorage
    {
        void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "_Default");
        PropertyDictionary Retrieve(ContainerIdentifier container, string name = "_Default");
    }
}
