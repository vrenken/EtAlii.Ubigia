namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class PropertiesDataClientStub : IPropertiesDataClient 
    {
        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            await Task.CompletedTask;
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return await Task.FromResult<PropertyDictionary>(null);
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }
    }
}
