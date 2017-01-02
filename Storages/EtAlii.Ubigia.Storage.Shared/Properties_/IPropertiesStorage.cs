namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IPropertiesStorage
    {
        void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "_Default");
        PropertyDictionary Retrieve(ContainerIdentifier container, string name = "_Default");
    }
}
