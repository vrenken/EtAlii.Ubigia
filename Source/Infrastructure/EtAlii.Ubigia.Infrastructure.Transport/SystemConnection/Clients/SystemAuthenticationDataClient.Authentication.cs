﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationDataClient
    {
        private string _authenticationToken;

        public async Task Authenticate(ISpaceConnection connection, string accountName, string password)
        {
            var authenticationToken = await GetAuthenticationToken(password).ConfigureAwait(false); // accountName, 

            if (!string.IsNullOrWhiteSpace(authenticationToken))
            {
                _authenticationToken = authenticationToken;
            }
            else
            {
                var message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        public async Task Authenticate(IStorageConnection connection, string accountName, string password)
        {
            var authenticationToken = await GetAuthenticationToken(password).ConfigureAwait(false); // accountName, 

            if (!string.IsNullOrWhiteSpace(authenticationToken))
            {
                _authenticationToken = authenticationToken;
            }
            else
            {
                var message = $"Unable to authenticate on the specified storage ({connection.Transport.Address})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
        }

        private Task<string> GetAuthenticationToken(
            //string accountName, 
            string password)
        {
            string authenticationToken;
            if (password == null && _authenticationToken != null)
            {
                authenticationToken = _authenticationToken;
            }
            else
            {
                authenticationToken = "System_" + Guid.NewGuid().ToString().Replace("-", "");
            }

            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return Task.FromResult(authenticationToken);
        }
    }
}
