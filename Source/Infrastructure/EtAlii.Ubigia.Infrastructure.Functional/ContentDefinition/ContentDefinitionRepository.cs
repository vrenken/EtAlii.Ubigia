// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class ContentDefinitionRepository : IContentDefinitionRepository
    {
        private readonly ILogicalContext _logicalContext;

        public ContentDefinitionRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        /// <inheritdoc />
        public async Task Store(Identifier identifier, ContentDefinition contentDefinition)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentDefinitionRepositoryException("No identifier specified");
            }

            if (contentDefinition == null)
            {
                throw new ContentDefinitionRepositoryException("No content definition specified");
            }

            if (contentDefinition.Stored)
            {
                throw new ContentDefinitionRepositoryException("Content definition already stored");
            }

            // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
            if (contentDefinition.Parts.Any(part => part.Stored))
            {
                throw new ContentDefinitionRepositoryException("Some parts of the content definition are already stored");
            }

            try
            {
                if (contentDefinition.Parts.Any())
                {
                    Blob.SetTotalParts(contentDefinition, (ulong)contentDefinition.Parts.Length);
                }

                // We need to clear the parts before they are stored. Else they are persisted in the content definition file itself.
                var contentDefinitionToStore = contentDefinition.ExceptParts();

                await _logicalContext.ContentDefinition.Store(identifier, contentDefinitionToStore).ConfigureAwait(false);

                // And of course the stored flag should be updated accordingly afterwards.
                Blob.SetStored(contentDefinition, contentDefinitionToStore.Stored);

                foreach (var contentDefinitionPart in contentDefinition.Parts)
                {
                    await _logicalContext.ContentDefinition.Store(identifier, contentDefinitionPart).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new ContentDefinitionRepositoryException("Unable to store the content definition for the specified identifier", e);
            }
        }

        /// <inheritdoc />
        public async Task Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentDefinitionRepositoryException("No identifier specified");
            }

            if (contentDefinitionPart == null)
            {
                throw new ContentDefinitionRepositoryException("No content definition part specified");
            }

            if (contentDefinitionPart.Stored)
            {
                throw new ContentDefinitionRepositoryException("Content definition part already stored");
            }

            try
            {
                var contentDefinition = await _logicalContext.ContentDefinition.Get(identifier).ConfigureAwait(false);
                if (contentDefinition == null)
                {
                    throw new ContentDefinitionRepositoryException("Content definition not stored yet");
                }
                if (contentDefinitionPart.Id >= contentDefinition.TotalParts)
                {
                    throw new ContentDefinitionRepositoryException("Content definition part has invalid Id");
                }
                await _logicalContext.ContentDefinition.Store(identifier, contentDefinitionPart).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new ContentDefinitionRepositoryException("Unable to store the content definition part for the specified identifier", e);
            }
        }

        /// <inheritdoc />
        public async Task<ContentDefinition> Get(Identifier identifier)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentDefinitionRepositoryException("No identifier specified");
            }

            try
            {
                var contentDefinition = await _logicalContext.ContentDefinition.Get(identifier).ConfigureAwait(false);

                if (contentDefinition != null)
                {
                    var parts = new List<ContentDefinitionPart>();
                    foreach (var contentDefinitionPartId in contentDefinition.Summary.AvailableParts)
                    {
                        var contentDefinitionPart = await _logicalContext.ContentDefinition.Get(identifier, contentDefinitionPartId).ConfigureAwait(false);
                        parts.Add(contentDefinitionPart);
                    }

                    contentDefinition = contentDefinition.WithPart(parts);
                }
                return contentDefinition;
            }
            catch (Exception e)
            {
                throw new ContentDefinitionRepositoryException("Unable to get the content definition for the specified identifier", e);
            }
        }
    }
}
