﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal abstract class SystemStorageClientBase
    {
        protected IStorageConnection Connection { get; private set; }

        public virtual Task Connect(IStorageConnection storageConnection)
        {
            Connection = storageConnection;
            return Task.CompletedTask;
        }

        public virtual Task Disconnect(IStorageConnection storageConnection)
        {
            Connection = null;
            return Task.CompletedTask;
        }
    }
}
