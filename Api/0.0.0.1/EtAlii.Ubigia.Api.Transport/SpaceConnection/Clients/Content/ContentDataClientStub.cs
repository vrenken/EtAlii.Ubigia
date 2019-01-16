namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class ContentDataClientStub : IContentDataClient 
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            await Task.Run(() => { });
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            await Task.Run(() => { });
        }

        public async Task<IReadOnlyContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return await Task.FromResult<IReadOnlyContentDefinition>(null);
        }

        public async Task Store(Identifier identifier, Content content)
        {
            await Task.Run(() => { });
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            await Task.Run(() => { });
        }

        public async Task<IReadOnlyContent> Retrieve(Identifier identifier)
        {
            return await Task.FromResult<IReadOnlyContent>(null);
        }

        public async Task<IReadOnlyContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return await Task.FromResult<IReadOnlyContentPart>(null);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }
    }
}
