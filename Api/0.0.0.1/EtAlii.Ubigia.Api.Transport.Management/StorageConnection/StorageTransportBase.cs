﻿namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public abstract class StorageTransportBase : IStorageTransport
    {
        public bool IsConnected { get; private set; }

        public virtual void Initialize(IStorageConnection storageConnection, string address)
        {
        }

        public virtual async Task Start(IStorageConnection storageConnection, string address)
        {
            await Task.Run(() => { IsConnected = true; });

        }

        public virtual async Task Stop(IStorageConnection storageConnection)
        {
            await Task.Run(() => { IsConnected = false; });
        }

        protected abstract IScaffolding[] CreateScaffoldingInternal();

        IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return CreateScaffoldingInternal();
        }
    }
}
