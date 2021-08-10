// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class NetCoreAppFactoryScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IStorageSerializer, NetCoreAppStorageSerializer>();
            container.RegisterDecorator<IStorageSerializer, LockingStorageSerializer>(); // We need file level locking.
            container.Register<IFolderManager, NetCoreAppFolderManager>();
            container.Register<IFileManager, NetCoreAppFileManager>();
            container.Register<IPathBuilder, NetCoreAppPathBuilder>();
            container.Register<IContainerProvider, DefaultContainerProvider>();

            container.Register<IItemSerializer, BsonItemSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();
        }
    }
}
