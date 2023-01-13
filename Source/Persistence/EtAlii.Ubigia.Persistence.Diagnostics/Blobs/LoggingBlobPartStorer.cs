// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using Serilog;

internal class LoggingBlobPartStorer : IBlobPartStorer
{
    private readonly IPathBuilder _pathBuilder;
    private readonly IBlobPartStorer _decoree;
    private readonly ILogger _logger = Log.ForContext<IBlobPartStorer>();

    public LoggingBlobPartStorer(
        IBlobPartStorer decoree,
        IPathBuilder pathBuilder)
    {
        _decoree = decoree;
        _pathBuilder = pathBuilder;
    }

    public void Store(ContainerIdentifier container, BlobPart blobPart)
    {
        var blobName = BlobPart.GetName(blobPart);
        var logContainer = ContainerIdentifier.Combine(container, blobName);
        var folder = _pathBuilder.GetFolder(logContainer);

        _logger.Verbose("Storing {BlobName} blob part in: {Folder}", blobName, folder);

        _decoree.Store(container, blobPart);
    }
}
