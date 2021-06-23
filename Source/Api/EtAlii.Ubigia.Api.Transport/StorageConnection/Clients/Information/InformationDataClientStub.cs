// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stub for the <see cref="IInformationDataClient"/>.
    /// </summary>
    public sealed class InformationDataClientStub : IInformationDataClient 
    {
        /// <inheritdoc />
        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<Storage> GetConnectedStorage(IStorageConnection connection)
        {
            return Task.FromResult<Storage>(null);
        }

        /// <inheritdoc />
        public Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
        {
            return Task.FromResult<ConnectivityDetails>(null);
        }
    }
}
