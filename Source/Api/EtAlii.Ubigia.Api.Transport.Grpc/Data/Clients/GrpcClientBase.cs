// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;

    public abstract class GrpcClientBase
    {
        protected ISpaceConnection<IGrpcSpaceTransport> Connection { get; private set; }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((ISpaceConnection<IGrpcSpaceTransport>)spaceConnection).ConfigureAwait(false);
        }

        public virtual Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            Connection = spaceConnection;
            return Task.CompletedTask;
        }

        public virtual Task Disconnect() 
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
