// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

internal class BlobPartStorer : IBlobPartStorer
{
    public const string FileNameFormat = "Part_{0}";

    private readonly IPathBuilder _pathBuilder;
    private readonly IImmutableFolderManager _folderManager;

    public BlobPartStorer(IImmutableFolderManager folderManager,
        IPathBuilder pathBuilder)
    {
        _folderManager = folderManager;
        _pathBuilder = pathBuilder;
    }

    public void Store(ContainerIdentifier container, BlobPart blobPart)
    {
        var blobName = BlobPart.GetName(blobPart);
        container = ContainerIdentifier.Combine(container, blobName);
        var folder = _pathBuilder.GetFolder(container);

        _folderManager.Create(folder);

        var fileName = string.Format(FileNameFormat, blobPart.Id);

        BlobPart.SetStored(blobPart, false);
        _folderManager.SaveToFolder(blobPart, fileName, folder);
        BlobPart.SetStored(blobPart, true);
    }
}
