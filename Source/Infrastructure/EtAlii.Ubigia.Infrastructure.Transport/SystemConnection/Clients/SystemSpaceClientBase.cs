// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal abstract class SystemSpaceClientBase
    {
        protected ISpaceConnection Connection { get; private set; }

        public virtual Task Connect(ISpaceConnection spaceConnection)
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
