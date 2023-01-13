// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

internal class ContainerCreator : IContainerCreator
{
    private readonly IImmutableFolderManager _folderManager;
    private readonly IPathBuilder _pathBuilder;

    public ContainerCreator(IImmutableFolderManager folderManager,
        IPathBuilder pathBuilder)
    {
        _folderManager = folderManager;
        _pathBuilder = pathBuilder;
    }

    public void Create(ContainerIdentifier containerToCreate)
    {
        var folder = _pathBuilder.GetFolder(containerToCreate);
        _folderManager.Create(folder);
    }
}
