namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class ContentDataClientStub : IContentDataClient 
    {
        public Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            return Task.CompletedTask;
        }

        public Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            return Task.CompletedTask;
        }

        public Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return Task.FromResult<IReadOnlyContentDefinition>(null);
        }

        public Task Store(Identifier identifier, Content content)
        {
            return Task.CompletedTask;
        }

        public Task Store(Identifier identifier, ContentPart contentPart)
        {
            return Task.CompletedTask;
        }

        public Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            return Task.FromResult<IReadOnlyContent>(null);
        }

        public Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return Task.FromResult<IReadOnlyContentPart>(null);
        }

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }
    }
}
