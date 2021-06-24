// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    public partial class RestAuthenticationDataClient : IAuthenticationDataClient
    {
        private IRestSpaceConnection _connection;

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Connect((IRestSpaceConnection) spaceConnection).ConfigureAwait(false);
        }

        private Task Connect(IRestSpaceConnection connection)
        {
            _connection = connection;
            return Task.CompletedTask;
        }

        public Task Disconnect()
        {
            _connection = null;
            return Task.CompletedTask;
        }
    }
}
