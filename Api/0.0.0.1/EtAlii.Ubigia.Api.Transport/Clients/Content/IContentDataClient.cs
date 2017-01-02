namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public interface IContentDataClient : ISpaceTransportClient
    {
        Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition);
        Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
        Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier);

        Task Store(Identifier identifier, Content content);
        Task Store(Identifier identifier, ContentPart contentPart);
        Task<IReadOnlyContent> Retrieve(Identifier identifier);
        Task<IReadOnlyContentPart> Retrieve(Identifier identifier, UInt64 contentPartId);
    }

    public interface IContentDataClient<in Ttransport> : IContentDataClient, ISpaceTransportClient<Ttransport>
        where Ttransport: ISpaceTransport
    {

    }
}