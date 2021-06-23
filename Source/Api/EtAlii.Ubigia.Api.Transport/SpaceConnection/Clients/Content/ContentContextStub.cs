// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class ContentContextStub : IContentContext
    {
        public IContentNotificationClient Notifications { get; }

        public IContentDataClient Data { get; }

        public ContentContextStub()
        {
            Notifications = new ContentNotificationClientStub();
            Data = new ContentDataClientStub();
        }

        public Task Open(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Close(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }
    }
}