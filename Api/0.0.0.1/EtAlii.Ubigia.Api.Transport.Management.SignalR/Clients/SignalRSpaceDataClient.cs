namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNet.SignalR.Client;

    public sealed class SignalRSpaceDataClient : ISpaceDataClient<ISignalRStorageTransport>
    {
        private IHubProxy _proxy;
        private readonly IHubProxyMethodInvoker _invoker;

        public SignalRSpaceDataClient(IHubProxyMethodInvoker invoker)
        {
            _invoker = invoker;
        }

        public async Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template)
        {
            var space = new Space
            {
                Name = spaceName,
                AccountId = accountId,
            };
            return await _invoker.Invoke<Space>(_proxy, SignalRHub.Space, "Post", space, template.Name);
        }

        public async Task Remove(Guid spaceId)
        {
            await _invoker.Invoke(_proxy, SignalRHub.Space, "Delete", spaceId);
        }

        public async Task<Space> Change(Guid spaceId, string spaceName)
        {
            var space = new Space
            {
                Id = spaceId,
                Name = spaceName,
            };
            return await _invoker.Invoke<Space>(_proxy, SignalRHub.Space, "Put", spaceId, space);
        }

        public async Task<Space> Get(Guid accountId, string spaceName)
        {
            return await _invoker.Invoke<Space>(_proxy, SignalRHub.Space, "GetForAccount", accountId, spaceName);
        }

        public async Task<Space> Get(Guid spaceId)
        {
            return await _invoker.Invoke<Space>(_proxy, SignalRHub.Space, "Get", spaceId);
        }

        public async Task<IEnumerable<Space>> GetAll(Guid accountId)
        {
            return await _invoker.Invoke<IEnumerable<Space>>(_proxy, SignalRHub.Space, "GetForAccount", accountId);
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>) storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>) storageConnection);
        }

        public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await Task.Run(() =>
            {
                _proxy = storageConnection.Transport.HubConnection.CreateHubProxy(SignalRHub.Space);
            });
        }

        public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            await Task.Run(() =>
            {
                _proxy = null;
            });
        }
    }
}
