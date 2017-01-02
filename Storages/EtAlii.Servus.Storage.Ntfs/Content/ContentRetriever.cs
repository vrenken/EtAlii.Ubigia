namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public class ContentRetriever : IContentRetriever
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IFolderManager _folderManager;

        public ContentRetriever(IFolderManager folderManager, IPathBuilder pathBuilder)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }

        public ContentDefinition Retrieve(ContainerIdentifier container)
        {
            throw new System.NotImplementedException();
        }

        public ContentPart Retrieve(ContainerIdentifier container, ContentPartDefinition contentPartDefinition)
        {
            throw new System.NotImplementedException();
        }
    }
}
