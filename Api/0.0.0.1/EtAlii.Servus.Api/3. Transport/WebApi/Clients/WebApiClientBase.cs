namespace EtAlii.Servus.Api.Transport.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal abstract class WebApiClientBase
    {
        protected ISpaceConnection<IWebApiSpaceTransport> Connection { get { return _connection; } }
        private ISpaceConnection<IWebApiSpaceTransport> _connection;

        protected WebApiClientBase()
        {
        }
        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await this.Connect((ISpaceConnection<IWebApiSpaceTransport>)spaceConnection);
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await this.Disconnect((ISpaceConnection<IWebApiSpaceTransport>)spaceConnection);
        }

        public virtual async Task Connect(ISpaceConnection<IWebApiSpaceTransport> spaceConnection)
        {
            await Task.Run(() => _connection = spaceConnection);
        }

        public virtual async Task Disconnect(ISpaceConnection<IWebApiSpaceTransport> spaceConnection)
        {
            await Task.Run(() => _connection = null);
        }
    }
}
