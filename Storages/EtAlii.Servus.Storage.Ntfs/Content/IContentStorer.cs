namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public interface IContentStorer
    {
        void Store(ContainerIdentifier container, ContentDefinition contentDefinition);
        void Store(ContainerIdentifier container, ContentPartDefinition contentPartDefinition, ContentPart contentPart);
    }
}
