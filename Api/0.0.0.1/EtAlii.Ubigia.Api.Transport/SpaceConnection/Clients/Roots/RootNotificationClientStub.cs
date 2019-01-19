﻿namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class RootNotificationClientStub : IRootNotificationClient
    {
        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }
    }
}
