namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Logical;

    internal class ContentRepository : IContentRepository
    {
        private readonly ILogicalContext _logicalContext;

        public ContentRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public void Store(Identifier identifier, Content content)
        {
            Store(identifier, content, new ContentPart[] { });
        }
        public void Store(Identifier identifier, Content content, IEnumerable<ContentPart> contentParts)
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

            if (contentParts.Any(part => part.Stored))
            {
                throw new ContentRepositoryException("Some parts of the content are already stored");
            }
            
            try
            {
                // We need to clear the parts before they are stored. Else they are persited in the content file itself.
                //var contentParts = new List<ContentPart>(content.Parts);
                //content.Parts.Clear();

                if (contentParts.Any())
                {
                    UInt64 totalParts = 0;
                    foreach (var contentPart in contentParts)
                    {
                        totalParts += 1;
                    }
                    content.TotalParts = totalParts;
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
        }

        public void Store(Identifier identifier, ContentPart contentPart)
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
                var content = _logicalContext.Content.Get(identifier) as Content;
                if (content == null)
                {
                    throw new ContentRepositoryException("Content not stored yet");
                }
                if (contentPart.Id >= content.TotalParts)
                {
                    throw new ContentRepositoryException("Content part has invalid Id");
                }
                if (content.Summary.AvailableParts.Any(partId => partId == contentPart.Id))
                {
                    throw new ContentRepositoryException("Content part already stored");
                }
                _logicalContext.Content.Store(identifier, contentPart);
            }
            catch (Exception e)
            {
                throw new ContentRepositoryException("Unable to store the content part for the specified identifier", e);
            }
        }

        public IReadOnlyContent Get(Identifier identifier)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentRepositoryException("No identifier specified");
            }

            try
            {
                return _logicalContext.Content.Get(identifier);
            }
            catch (Exception e)
            {
                throw new ContentRepositoryException("Unable to get the content for the specified identifier", e);
            }
        }

        public IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentRepositoryException("No identifier specified");
            }

            try
            {
                return _logicalContext.Content.Get(identifier, contentPartId);
            }
            catch (Exception e)
            {
                throw new ContentRepositoryException("Unable to get the content part for the specified identifier", e);
            }
        }
    }
}