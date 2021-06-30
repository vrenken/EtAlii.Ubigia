// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryFactoryScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IStorageSerializer, InMemoryStorageSerializer>();
            container.RegisterDecorator(typeof(IStorageSerializer), typeof(LockingStorageSerializer)); // We need file level locking.
            container.Register<IFolderManager, InMemoryFolderManager>();
            container.Register<IFileManager, InMemoryFileManager>();
            container.Register<IPathBuilder, InMemoryPathBuilder>();
            container.Register<IContainerProvider, DefaultContainerProvider>();

            container.Register<IItemSerializer, BsonItemSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();

            container.Register<IInMemoryItems, InMemoryItems>();
            container.Register<IInMemoryItemsHelper, InMemoryItemsHelper>();
        }
    }
}
