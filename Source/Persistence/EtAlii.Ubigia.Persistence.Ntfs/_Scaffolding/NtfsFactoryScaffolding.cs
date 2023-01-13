// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Ntfs;

using EtAlii.xTechnology.MicroContainer;

public class NtfsFactoryScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IStorageSerializer, NtfsStorageSerializer>();
        container.RegisterDecorator<IStorageSerializer, LockingStorageSerializer>(); // We need file level locking.
        container.Register<IFolderManager, NtfsFolderManager>();
        container.Register<IFileManager, NtfsFileManager>();
        container.Register<IPathBuilder, NtfsPathBuilder>();
        container.Register<IContainerProvider, DefaultContainerProvider>();

        container.Register<IItemSerializer, ItemSerializer>();
        container.Register<IPropertiesSerializer, PropertiesSerializer>();
    }
}
