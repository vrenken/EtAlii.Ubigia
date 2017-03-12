namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class SignalRPropertiesDataClient : SignalRClientBase, IPropertiesDataClient<ISignalRSpaceTransport>
    {
        private IHubProxy _proxy;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRPropertiesDataClient(
            IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }


        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            await _invoker.Invoke(_proxy, SignalRHub.Property, "Post", identifier, properties);

            PropertiesHelper.SetStored(properties, true);
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return await scope.Cache.GetProperties(identifier, async () =>
            {
                var result = await _invoker.Invoke<PropertyDictionary>(_proxy, SignalRHub.Property, "Get", identifier);
                if (result != null)
                {
                    PropertiesHelper.SetStored(result, true);
                    // properties.Stored is not serialized in the PropertyDictionaryConverter.
                }
                return result;
            });
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = spaceConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Property);
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = null;
            });
        }
    }
}
