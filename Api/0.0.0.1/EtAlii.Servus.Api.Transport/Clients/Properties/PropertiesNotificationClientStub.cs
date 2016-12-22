﻿namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class PropertiesNotificationClientStub : IPropertiesNotificationClient
    {
        public event Action<Identifier> Stored = delegate { };

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => { });
        }
    }
}
