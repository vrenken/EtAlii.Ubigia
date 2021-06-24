// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class SharedFactoryScaffolding<TSerializer, TFolderManager, TFileManager, TPathBuilder, TContainerProvider> : IScaffolding
        where TSerializer : class, IStorageSerializer
        where TFolderManager : class, IFolderManager
        where TFileManager : class, IFileManager
        where TPathBuilder : class, IPathBuilder
        where TContainerProvider : class, IContainerProvider
    {
        public void Register(Container container)
        {
            container.Register<IStorageSerializer, TSerializer>();
            container.RegisterDecorator(typeof(IStorageSerializer), typeof(LockingStorageSerializer)); // We need file level locking.
            container.Register<IFolderManager, TFolderManager>();
            container.Register<IFileManager, TFileManager>();
            container.Register<IPathBuilder, TPathBuilder>();
            container.Register<IContainerProvider, TContainerProvider>();

            container.Register<IItemSerializer, BsonItemSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();
        }
    }
}
