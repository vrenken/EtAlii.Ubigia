namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;

    public interface IContentContext
    {
        void StoreDefinition(Identifier identifier, ContentDefinition contentDefinition);
        void StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        IReadOnlyContentDefinition RetrieveDefinition(Identifier identifier);

        void Store(Identifier identifier, EtAlii.Servus.Api.Content content);
        void Store(Identifier identifier, ContentPart contentPart);
        IReadOnlyContent Retrieve(Identifier identifier);
        IReadOnlyContentPart Retrieve(Identifier identifier, UInt64 contentPartId);

        event Action<Identifier> Updated;
        event Action<Identifier> Stored;
    }
}
