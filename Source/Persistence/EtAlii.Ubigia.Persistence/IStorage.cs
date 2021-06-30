// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    public interface IStorage
    {
        IStorageConfiguration Configuration { get; }
        IPathBuilder PathBuilder { get; }
        IImmutableFileManager FileManager { get; }
        IImmutableFolderManager FolderManager { get; }
        // Make the IStorage.StorageSerializer internal somehow
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/83
        IStorageSerializer StorageSerializer { get; }

        IPropertiesStorage Properties { get; }

        IItemStorage Items { get; }
        IComponentStorage Components { get; }
        IBlobStorage Blobs { get; }
        IContainerProvider ContainerProvider { get; }
    }
}
