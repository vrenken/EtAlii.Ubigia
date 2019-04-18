﻿namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;

    public abstract class GrpcManagementClientBase
    {
        //protected IStorageConnection<IGrpcStorageTransport> Connection { get; private set; }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Connect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Disconnect((IStorageConnection<IGrpcStorageTransport>)storageConnection);
        }

        public virtual Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            //Connection = storageConnection
            return Task.CompletedTask;
        }

        public virtual Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            //Connection = null
            return Task.CompletedTask;
        }
    }
}
