// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Standard
{
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    public class StandardFactoryScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IStorageSerializer, StandardStorageSerializer>();
            container.RegisterDecorator<IStorageSerializer, LockingStorageSerializer>(); // We need file level locking.
            container.Register<IFolderManager, StandardFolderManager>();
            container.Register<IFileManager, StandardFileManager>();
            container.Register<IPathBuilder, StandardPathBuilder>();
            container.Register<IContainerProvider, DefaultContainerProvider>();

            container.Register<IItemSerializer, BsonItemSerializer>();
            container.Register<IPropertiesSerializer, BsonPropertiesSerializer>();
        }
    }
}
