// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;
    using PCLStorage;

    public class PortableFactoryScaffolding : IScaffolding
    {
        private readonly IFolder _localStorage;

        internal PortableFactoryScaffolding(IFolder localStorage)
        {
            _localStorage = localStorage;
        }

        public void Register(Container container)
        {
            container.Register<IStorageSerializer, PortableStorageSerializer>();
            container.RegisterDecorator(typeof(IStorageSerializer), typeof(LockingStorageSerializer)); // We need file level locking.
            container.Register<IFolderManager, PortableFolderManager>();
            container.Register<IFileManager, PortableFileManager>();
            container.Register<IPathBuilder, PortablePathBuilder>();
            container.Register<IContainerProvider, PortableContainerProvider>();

            container.Register<IItemSerializer, BsonItemSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();

            container.Register(() => _localStorage);
        }
    }
}
