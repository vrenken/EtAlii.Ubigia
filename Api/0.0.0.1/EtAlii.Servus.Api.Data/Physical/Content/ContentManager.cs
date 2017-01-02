namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.IO;

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

        public void Upload(Stream stream, UInt64 sizeInBytes, Identifier identifier)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentManagerException("No identifier specified");
            }

            var requiredParts = _contentPartCalculator.GetRequiredParts(sizeInBytes);
            var partSize = _contentPartCalculator.GetPartSize(sizeInBytes);

            var contentDefinition = _contentDefinitionQueryHandler.Execute(new ContentDefinitionQuery(identifier, sizeInBytes, requiredParts, partSize));
            var content = _contentNewQueryHandler.Execute(new ContentNewQuery(identifier, sizeInBytes, requiredParts, partSize));

            _contentPartStoreCommandHandler.Execute(new ContentPartStoreCommand(stream, sizeInBytes, requiredParts, partSize, identifier, contentDefinition, content));
        }


        public void Download(Stream stream, Identifier identifier, bool validateChecksum = false)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentManagerException("No identifier specified");
            }

            if (validateChecksum)
            {
                throw new NotImplementedException();
            }

            var content = _contentQueryHandler.Execute(new ContentQuery(identifier));
            if (content != null && content.Summary.IsComplete)
            {
                _contentPartQueryHandler.Execute(new ContentPartQuery(stream, identifier, content));
            }
        }
    }
}
