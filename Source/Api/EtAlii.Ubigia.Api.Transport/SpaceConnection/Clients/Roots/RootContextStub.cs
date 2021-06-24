// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class RootContextStub : IRootContext
    {
        /// <inheritdoc />
        public IRootNotificationClient Notifications { get; }

        /// <inheritdoc />
        public IRootDataClient Data { get; }

        /// <summary>
        /// Create a new <see cref="RootContextStub"/> instance.
        /// </summary>
        public RootContextStub()
        {
            Notifications = new RootNotificationClientStub();
            Data = new RootDataClientStub();
        }

        /// <inheritdoc />
        public Task Open(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Close(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }
    }
}
