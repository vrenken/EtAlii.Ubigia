namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    public interface IContentContext
    {
        Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition);
        Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier);

        Task Store(Identifier identifier, Content content);
        Task Store(Identifier identifier, ContentPart contentPart);
        Task<IReadOnlyContent> Retrieve(Identifier identifier);
        Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId);

        event Action<Identifier> Updated;
        event Action<Identifier> Stored;
    }
}
