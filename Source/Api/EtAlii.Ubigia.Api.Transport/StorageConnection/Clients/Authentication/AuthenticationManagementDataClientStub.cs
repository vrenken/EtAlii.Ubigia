// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class AuthenticationManagementDataClientStub : IAuthenticationManagementDataClient
    {
        /// <inheritdoc />
        public Task Authenticate(IStorageConnection storageConnection, string accountName, string password)
        {
            return Task.CompletedTask;
        }
        
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
    }
}
