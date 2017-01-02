namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public interface IContentRetriever
    {
        ContentDefinition Retrieve(ContainerIdentifier container);
        ContentPart Retrieve(ContainerIdentifier container, ContentPartDefinition contentPartDefinition);
    }
}
