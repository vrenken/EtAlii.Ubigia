// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public sealed class SpaceNotificationClientStub : ISpaceNotificationClient 
    {
        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
