// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;

    public interface ISimpleAuthenticationTokenVerifier
    {
        Task<Account> Verify(string authenticationTokenAsString, params string[] requiredRoles);
    }
}
