// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public abstract class SignalRClientBase
    {
        protected ISpaceConnection<ISignalRSpaceTransport> Connection { get; private set; }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<ISignalRSpaceTransport>)spaceConnection).ConfigureAwait(false);
        }

        public virtual Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            Connection = spaceConnection;
            return Task.CompletedTask;
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Disconnect().ConfigureAwait(false); 
        }

        public virtual Task Disconnect()
        {
            Connection = null;            
            return Task.CompletedTask;
        }
    }
}
