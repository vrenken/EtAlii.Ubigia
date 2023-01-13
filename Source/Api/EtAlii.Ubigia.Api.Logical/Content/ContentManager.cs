// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.IO;
using System.Threading.Tasks;

internal class ContentManager : IContentManager
{
    private readonly IContentPartCalculator _contentPartCalculator;
    private readonly IContentPartStoreCommandHandler _contentPartStoreCommandHandler;
    private readonly IContentPartQueryHandler _contentPartQueryHandler;
    private readonly IContentDefinitionQueryHandler _contentDefinitionQueryHandler;
    private readonly IContentQueryHandler _contentQueryHandler;
    private readonly IContentNewQueryHandler _contentNewQueryHandler;


    public ContentManager(
        IContentPartCalculator contentPartCalculator,
        IContentPartStoreCommandHandler contentPartStoreCommandHandler,
        IContentPartQueryHandler contentPartQueryHandler,
        IContentDefinitionQueryHandler contentDefinitionQueryHandler,
        IContentQueryHandler contentQueryHandler,
        IContentNewQueryHandler contentNewQueryHandler)
    {
        _contentPartCalculator = contentPartCalculator;
        _contentPartStoreCommandHandler = contentPartStoreCommandHandler;
        _contentPartQueryHandler = contentPartQueryHandler;
        _contentDefinitionQueryHandler = contentDefinitionQueryHandler;
        _contentQueryHandler = contentQueryHandler;
        _contentNewQueryHandler = contentNewQueryHandler;
    }

    public async Task Upload(Stream stream, ulong sizeInBytes, Identifier identifier)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentManagerException("No identifier specified");
        }

        var requiredParts = _contentPartCalculator.GetRequiredParts(sizeInBytes);
        var partSize = _contentPartCalculator.GetPartSize(sizeInBytes);

        var contentDefinition = await _contentDefinitionQueryHandler.Execute(new ContentDefinitionQuery(identifier, sizeInBytes, requiredParts, partSize)).ConfigureAwait(false);
        var content = await _contentNewQueryHandler.Execute(new ContentNewQuery(identifier, sizeInBytes, requiredParts, partSize)).ConfigureAwait(false);

        await _contentPartStoreCommandHandler.Execute(new ContentPartStoreCommand(stream, sizeInBytes, requiredParts, partSize, identifier, contentDefinition, content)).ConfigureAwait(false);
    }

    public async Task Download(Stream stream, Identifier identifier, bool validateChecksum = false)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentManagerException("No identifier specified");
        }

        if (validateChecksum)
        {
            throw new NotImplementedException();
        }

        var content = await _contentQueryHandler.Execute(new ContentQuery(identifier)).ConfigureAwait(false);
        if (content != null && content.Summary.IsComplete)
        {
            await _contentPartQueryHandler.Execute(new ContentPartQuery(stream, identifier, content)).ConfigureAwait(false);
        }
    }

    public Task<bool> HasContent(Identifier identifier)
    {
        if (identifier == Identifier.Empty)
        {
            throw new PropertyManagerException("No identifier specified");
        }

        throw new NotImplementedException();
    }
}
