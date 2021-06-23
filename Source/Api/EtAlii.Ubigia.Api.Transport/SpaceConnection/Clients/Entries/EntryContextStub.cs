// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class EntryContextStub : IEntryContext
    {
        /// <inheritdoc />
        public IEntryNotificationClient Notifications { get; }

        /// <inheritdoc />
        public IEntryDataClient Data { get; }

        /// <summary>
        /// Create a new <see cref="EntryContextStub"/> instance. 
        /// </summary>
        public EntryContextStub()
        {
            Notifications = new EntryNotificationClientStub();
            Data = new EntryDataClientStub();
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