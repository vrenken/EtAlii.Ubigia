// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

    public class SimpleAuthenticationBuilder : ISimpleAuthenticationBuilder
	{
        //private readonly IAccountRepository _accountRepository
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public SimpleAuthenticationBuilder(
            //IAccountRepository accountRepository,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            //_accountRepository = accountRepository
            _authenticationTokenConverter = authenticationTokenConverter;
        }
		
        public string Build(string accountName, string hostIdentifier)
        {
            try
            {
                var success = !string.IsNullOrWhiteSpace(hostIdentifier);
                if (success)
                {
                    var authenticationToken = new AuthenticationToken
                    {
                        Name = accountName,
                        Address = hostIdentifier,
                        Salt = DateTime.UtcNow.ToBinary(),
                    };

                    var authenticationTokenAsBytes = _authenticationTokenConverter.ToBytes(authenticationToken);
                    authenticationTokenAsBytes = Aes.Encrypt(authenticationTokenAsBytes);
                    var authenticationTokenAsString = Convert.ToBase64String(authenticationTokenAsBytes);
                    return authenticationTokenAsString;
                }
                else
                {
                    throw new UnauthorizedInfrastructureOperationException("Invalid identifier");
                }
            }
            catch (Exception e)
            {
                throw new UnauthorizedInfrastructureOperationException("Unauthorized", e);
            }
        }
    }
}