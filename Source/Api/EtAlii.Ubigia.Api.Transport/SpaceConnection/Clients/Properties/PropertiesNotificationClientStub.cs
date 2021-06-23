// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed notification client that can be used to monitor for property notifications.
    /// </summary>
    public class PropertiesNotificationClientStub : IPropertiesNotificationClient
    {
        /// <inheritdoc />
        public event Action<Identifier> Stored = delegate { };

        /// <inheritdoc />
        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
