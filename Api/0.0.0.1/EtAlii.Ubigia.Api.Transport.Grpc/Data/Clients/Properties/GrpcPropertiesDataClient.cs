namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    internal class GrpcPropertiesDataClient : GrpcClientBase, IPropertiesDataClient<IGrpcSpaceTransport>
    {
        //private HubConnection _connection;
        //private readonly IHubProxyMethodInvoker _invoker;

        //public GrpcPropertiesDataClient(
        //    IHubProxyMethodInvoker invoker)
        //{
        //    _invoker = invoker;
        //}


        public async Task Store(Api.Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            // TODO: GRPC
            //await _invoker.Invoke(_connection, GrpcHub.Property, "Post", identifier, properties);

            PropertiesHelper.SetStored(properties, true);
        }

        public async Task<PropertyDictionary> Retrieve(Api.Identifier identifier, ExecutionScope scope)
        {
            return await scope.Cache.GetProperties(identifier, async () =>
            {
                // TODO: GRPC
                var result = await Task.FromResult<PropertyDictionary>(null);
                //var result = await _invoker.Invoke<PropertyDictionary>(_connection, GrpcHub.Property, "Get", identifier);
                if (result != null)
                {
                    PropertiesHelper.SetStored(result, true);
                    // properties.Stored is not serialized in the PropertyDictionaryConverter.
                }
                return result;
            });
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
            //_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Property, UriKind.Absolute));
	        //await _connection.StartAsync();
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            // TODO: GRPC
            //await _connection.DisposeAsync();
            //_connection = null;
        }
    }
}
