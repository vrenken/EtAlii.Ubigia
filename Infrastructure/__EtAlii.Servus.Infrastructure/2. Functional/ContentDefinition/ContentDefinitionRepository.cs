namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Logical;

    internal class ContentDefinitionRepository : IContentDefinitionRepository
    {
        private readonly ILogicalContext _logicalContext;

        public ContentDefinitionRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public void Store(Identifier identifier, ContentDefinition contentDefinition)
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

            if (contentDefinition.Parts.Any(part => part.Stored))
            {
                throw new ContentDefinitionRepositoryException("Some parts of the content definition are already stored");
            }

            try
            {
                // We need to clear the parts before they are stored. Else they are persited in the contentdefinition file itself.
                var contentDefinitionParts = new List<ContentDefinitionPart>(contentDefinition.Parts);
                contentDefinition.Parts.Clear();

                if (contentDefinitionParts.Any())
                {
                    UInt64 totalParts = 0;
                    foreach (var contentDefinitionPart in contentDefinitionParts)
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

        public void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
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
                var contentDefinition = _logicalContext.ContentDefinition.Get(identifier) as ContentDefinition;
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

        public IReadOnlyContentDefinition Get(Identifier identifier)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentDefinitionRepositoryException("No identifier specified");
            }

            try
            {
                var contentDefinition = (ContentDefinition)_logicalContext.ContentDefinition.Get(identifier);

                if (contentDefinition != null)
                {
                    foreach (UInt64 contentDefinitionPartId in contentDefinition.Summary.AvailableParts)
                    {
                        var contentDefinitionPart = (ContentDefinitionPart)_logicalContext.ContentDefinition.Get(identifier, contentDefinitionPartId);
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