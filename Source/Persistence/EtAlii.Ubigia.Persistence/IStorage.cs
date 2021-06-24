// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    public interface IStorage
    {
        IStorageConfiguration Configuration { get; }
        IPathBuilder PathBuilder { get; }
        IImmutableFileManager FileManager { get; }
        IImmutableFolderManager FolderManager { get; }
        IStorageSerializer StorageSerializer { get; } // TODO: Should be made internal

        IPropertiesStorage Properties { get; }

        IItemStorage Items { get; }
        IComponentStorage Components { get; }
        IBlobStorage Blobs { get; }
        IContainerProvider ContainerProvider { get; }
    }
}
