// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class AzureFactoryScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IStorageSerializer, AzureStorageSerializer>();
            container.RegisterDecorator<IStorageSerializer, LockingStorageSerializer>(); // We need file level locking.
            container.Register<IFolderManager, AzureFolderManager>();
            container.Register<IFileManager, AzureFileManager>();
            container.Register<IPathBuilder, AzurePathBuilder>();
            container.Register<IContainerProvider, DefaultContainerProvider>();

            container.Register<IItemSerializer, BsonItemSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();
        }
    }
}
