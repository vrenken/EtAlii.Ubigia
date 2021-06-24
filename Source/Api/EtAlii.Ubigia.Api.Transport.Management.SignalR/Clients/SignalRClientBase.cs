// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public abstract class SignalRManagementClientBase
    {
        protected IStorageConnection<ISignalRStorageTransport> Connection { get; private set; }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<ISignalRStorageTransport>)storageConnection).ConfigureAwait(false);
        }

        public virtual Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            Connection = storageConnection;
            return Task.CompletedTask;
        }
        
        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<ISignalRStorageTransport>)storageConnection).ConfigureAwait(false);
        }

        public virtual Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
