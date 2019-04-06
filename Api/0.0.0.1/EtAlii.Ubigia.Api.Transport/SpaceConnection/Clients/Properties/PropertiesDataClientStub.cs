namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class PropertiesDataClientStub : IPropertiesDataClient 
    {
        public Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            return Task.CompletedTask;
        }

        public Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return Task.FromResult<PropertyDictionary>(null);
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
