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

        public void Store(in Identifier identifier, ContentDefinition contentDefinition)
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
                // We need to clear the parts before they are stored. Else they are persisted in the content definition file itself.
                var contentDefinitionParts = new List<ContentDefinitionPart>(contentDefinition.Parts);
                contentDefinition.Parts.Clear();

                if (contentDefinitionParts.Any())
                {
                    ulong totalParts = 0;
                    foreach (var _ in contentDefinitionParts)
                    {
                        totalParts += 1;
                    }
                    contentDefinition.TotalParts = totalParts;
                }

                _logicalContext.ContentDefinition.Store(identifier, contentDefinition);

                foreach (var contentDefinitionPart in contentDefinitionParts)
                {
                    _logicalContext.ContentDefinition.Store(identifier, contentDefinitionPart);
                }
            }
            catch (Exception e)
            {
                throw new ContentDefinitionRepositoryException("Unable to store the content definition for the specified identifier", e);
            }
        }

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
                var contentDefinition = await _logicalContext.ContentDefinition.Get(identifier).ConfigureAwait(false) as ContentDefinition;
                if (contentDefinition == null)
                {
                    throw new ContentDefinitionRepositoryException("Content definition not stored yet");
                }
                if (contentDefinitionPart.Id >= contentDefinition.TotalParts)
                {
                    throw new ContentDefinitionRepositoryException("Content definition part has invalid Id");
                }
                _logicalContext.ContentDefinition.Store(identifier, contentDefinitionPart);
            }
            catch (Exception e)
            {
                throw new ContentDefinitionRepositoryException("Unable to store the content definition part for the specified identifier", e);
            }
        }

        public async Task<IReadOnlyContentDefinition> Get(Identifier identifier)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentDefinitionRepositoryException("No identifier specified");
            }

            try
            {
                var contentDefinition = (ContentDefinition)await _logicalContext.ContentDefinition.Get(identifier).ConfigureAwait(false);

                if (contentDefinition != null)
                {
                    foreach (var contentDefinitionPartId in contentDefinition.Summary.AvailableParts)
                    {
                        var contentDefinitionPart = (ContentDefinitionPart)await _logicalContext.ContentDefinition.Get(identifier, contentDefinitionPartId).ConfigureAwait(false);
                        contentDefinition.Parts.Add(contentDefinitionPart);
                    }
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