// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.Threading.Tasks;

internal class BlobPartRetriever : IBlobPartRetriever
{
    private readonly IPathBuilder _pathBuilder;
    private readonly IImmutableFileManager _fileManager;

    public BlobPartRetriever(IImmutableFileManager fileManager, IPathBuilder pathBuilder)
    {
        _fileManager = fileManager;
        _pathBuilder = pathBuilder;
    }

    public async Task<T> Retrieve<T>(ContainerIdentifier container, ulong position)
        where T : BlobPart
    {
        var blobName = BlobPart.GetName<T>();
        container = ContainerIdentifier.Combine(container, blobName);

        T blobPart = null;

        var fileName = string.Format(BlobPartStorer.FileNameFormat, position);

        var path = _pathBuilder.GetFileName(fileName, container);
        if (_fileManager.Exists(path))
        {
            blobPart = await _fileManager
                .LoadFromFile<T>(path)
                .ConfigureAwait(false);
            BlobPart.SetStored(blobPart, true);
        }

        return blobPart;
    }
}
