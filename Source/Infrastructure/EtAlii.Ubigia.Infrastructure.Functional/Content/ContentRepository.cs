// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class ContentRepository : IContentRepository
{
    private readonly ILogicalContext _logicalContext;

    public ContentRepository(ILogicalContext logicalContext)
    {
        _logicalContext = logicalContext;
    }

    /// <inheritdoc />
    public Task Store(Identifier identifier, Content content)
    {
        return Store(identifier, content, Array.Empty<ContentPart>());
    }

    /// <inheritdoc />
    public Task Store(Identifier identifier, Content content, ContentPart[] contentParts)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentRepositoryException("No identifier specified");
        }

        if (content == null)
        {
            throw new ContentRepositoryException("No content specified");
        }

        if (contentParts == null)
        {
            throw new ContentRepositoryException("No content parts specified");
        }

        if (content.Stored)
        {
            throw new ContentRepositoryException("Content already stored");
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        if (contentParts.Any(part => part.Stored))
        {
            throw new ContentRepositoryException("Some parts of the content are already stored");
        }

        try
        {
            // We need to clear the parts before they are stored. Else they are persisted in the content file itself.
            //var contentParts = new List<ContentPart>(content.Parts)
            //content.Parts.Clear()

            if (contentParts.Any())
            {
                Blob.SetTotalParts(content, (ulong)contentParts.Length);
            }

            _logicalContext.Content.Store(identifier, content);

            foreach (var contentPart in contentParts)
            {
                _logicalContext.Content.Store(identifier, contentPart);
            }
        }
        catch (Exception e)
        {
            throw new ContentRepositoryException("Unable to store the content for the specified identifier", e);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task Store(Identifier identifier, ContentPart contentPart)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentRepositoryException("No identifier specified");
        }

        if (contentPart == null)
        {
            throw new ContentDefinitionRepositoryException("No content part specified");
        }

        if (contentPart.Stored)
        {
            throw new ContentDefinitionRepositoryException("Content part already stored");
        }

        try
        {
            var content = await _logicalContext.Content.Get(identifier).ConfigureAwait(false);
            if (content == null)
            {
                throw new ContentRepositoryException("Content not stored yet");
            }
            if (contentPart.Id >= content.TotalParts)
            {
                throw new ContentRepositoryException("Content part has invalid Id");
            }
            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            if (content.Summary.AvailableParts.Any(partId => partId == contentPart.Id))
            {
                throw new ContentRepositoryException("Content part already stored");
            }
            await _logicalContext.Content.Store(identifier, contentPart).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new ContentRepositoryException("Unable to store the content part for the specified identifier", e);
        }
    }

    /// <inheritdoc />
    public async Task<Content> Get(Identifier identifier)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentRepositoryException("No identifier specified");
        }

        try
        {
            return await _logicalContext.Content.Get(identifier).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new ContentRepositoryException("Unable to get the content for the specified identifier", e);
        }
    }

    /// <inheritdoc />
    public async Task<ContentPart> Get(Identifier identifier, ulong contentPartId)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentRepositoryException("No identifier specified");
        }

        try
        {
            return await _logicalContext.Content.Get(identifier, contentPartId).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new ContentRepositoryException("Unable to get the content part for the specified identifier", e);
        }
    }
}
