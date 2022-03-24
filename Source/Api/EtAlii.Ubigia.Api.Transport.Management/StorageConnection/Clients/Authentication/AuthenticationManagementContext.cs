// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System.Threading.Tasks;

    public sealed class AuthenticationManagementContext : IAuthenticationManagementContext
    {
        public IAuthenticationManagementDataClient Data { get; }

        public AuthenticationManagementContext(IAuthenticationManagementDataClient data)
        {
            Data = data;
        }

        public async Task Open(IStorageConnection storageConnection)
        {
            await Data.Connect(storageConnection).ConfigureAwait(false);
        }

        public async Task Close(IStorageConnection storageConnection)
        {
            await Data.Disconnect(storageConnection).ConfigureAwait(false);
        }

    }

}
