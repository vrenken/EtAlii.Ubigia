// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryFactoryScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IStorageSerializer, InMemoryStorageSerializer>();
            container.RegisterDecorator<IStorageSerializer, LockingStorageSerializer>(); // We need file level locking.
            container.Register<IFolderManager, InMemoryFolderManager>();
            container.Register<IFileManager, InMemoryFileManager>();
            container.Register<IPathBuilder, InMemoryPathBuilder>();
            container.Register<IContainerProvider, DefaultContainerProvider>();

            container.Register<IItemSerializer, ItemSerializer>();
            container.Register<IPropertiesSerializer, PropertiesSerializer>();

            container.Register<IInMemoryItems, InMemoryItems>();
            container.Register<IInMemoryItemsHelper, InMemoryItemsHelper>();
        }
    }
}
