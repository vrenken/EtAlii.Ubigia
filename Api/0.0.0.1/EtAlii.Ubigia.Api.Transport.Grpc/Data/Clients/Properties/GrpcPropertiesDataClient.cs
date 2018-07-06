namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    
    internal class GrpcPropertiesDataClient : GrpcClientBase, IPropertiesDataClient<IGrpcSpaceTransport>
    {
        private IGrpcSpaceTransport _transport;
        private PropertiesGrpcService.PropertiesGrpcServiceClient _client;

        public async Task Store(Api.Identifier identifier, Api.PropertyDictionary properties, ExecutionScope scope)
        {
            var request = new PropertiesPostRequest{EntryId = identifier.ToWire(), PropertyDictionary = properties.ToWire()};
            await _client.PostAsync(request, _transport.AuthenticationHeaders);
            //await _invoker.Invoke(_connection, GrpcHub.Property, "Post", identifier, properties);
            PropertiesHelper.SetStored(properties, true);
        }

        public async Task<Api.PropertyDictionary> Retrieve(Api.Identifier identifier, ExecutionScope scope)
        {
            return await scope.Cache.GetProperties(identifier, async () =>
            {
                var request = new PropertiesGetRequest{ EntryId = identifier.ToWire() };
                var response = await _client.GetAsync(request, _transport.AuthenticationHeaders);
                var result = response.PropertyDictionary.ToLocal();
                
                if (result != null)
                {
                    PropertiesHelper.SetStored(result, true);
                    // properties.Stored is not serialized in the PropertyDictionaryConverter.
                }
                return result;
            });
        }

        public override Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _transport = ((IGrpcSpaceConnection) spaceConnection).Transport;
            _client = new PropertiesGrpcService.PropertiesGrpcServiceClient(_transport.Channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _transport = null;
            _client = null;
            return Task.CompletedTask;
        }
    }
}
