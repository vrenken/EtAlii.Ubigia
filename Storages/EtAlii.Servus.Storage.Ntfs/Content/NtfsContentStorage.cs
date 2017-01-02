namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public partial class NtfsContentStorage : IContentStorage
    {
        private readonly IContentStorer _contentStorer;

        public NtfsContentStorage(IContentStorer contentStorer)
        {
            _contentStorer = contentStorer;
        }

        public ContentDefinition Retrieve(ContainerIdentifier container)
        {
            throw new System.NotImplementedException();
        }

        public void Store(ContainerIdentifier container, ContentDefinition contentDefinition)
        {
            _contentStorer.Store(container, contentDefinition);
        }

        public ContentPart Retrieve(ContainerIdentifier container, ContentPartDefinition contentPartDefinition)
        {
            throw new System.NotImplementedException();
        }

        public void Store(ContainerIdentifier container, ContentPartDefinition contentPartDefinition, ContentPart contentPart)
        {
            _contentStorer.Store(container, contentPartDefinition, contentPart);
        }
    }
}
