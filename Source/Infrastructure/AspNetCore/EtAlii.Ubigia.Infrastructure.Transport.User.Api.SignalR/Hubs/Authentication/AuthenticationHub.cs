// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR;

using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;
using Microsoft.AspNetCore.SignalR;

public class AuthenticationHub : Hub
{
    private readonly IStorageRepository _storageRepository;

    private readonly ISimpleAuthenticationVerifier _authenticationVerifier;
    private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;
    private readonly ISimpleAuthenticationBuilder _authenticationBuilder;

    public AuthenticationHub(
        ISimpleAuthenticationVerifier authenticationVerifier,
        ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
        IStorageRepository storageRepository,
        ISimpleAuthenticationBuilder authenticationBuilder)
    {
        _authenticationVerifier = authenticationVerifier;
        _authenticationTokenVerifier = authenticationTokenVerifier;
        _storageRepository = storageRepository;
        _authenticationBuilder = authenticationBuilder;
    }

    public async Task<string> Authenticate(string accountName, string password, string hostIdentifier)
    {
        var (response, _) = await _authenticationVerifier
            .Verify(accountName, password, hostIdentifier, Role.User, Role.System)
            .ConfigureAwait(false);
        return response;
    }

    public async Task<string> AuthenticateAs(string accountName, string hostIdentifier)
    {
        var httpContext = Context.GetHttpContext();
        httpContext!.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
        var authenticationToken = stringValues.Single();
        await _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System).ConfigureAwait(false);

        return _authenticationBuilder.Build(accountName, hostIdentifier);
    }

    public async Task<Storage> GetLocalStorage()
    {
        var httpContext = Context.GetHttpContext();
        httpContext!.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
        var authenticationToken = stringValues.Single();
        await _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System).ConfigureAwait(false);

        return await _storageRepository.GetLocal().ConfigureAwait(false);
    }
}
