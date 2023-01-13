// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Rest;

using Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAttributeBasedInfrastructureAuthorization(this IServiceCollection services)
    {
        services
            .AddSingleton<IAuthenticationTokenConverter, AuthenticationTokenConverter>()
            .AddSingleton<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>()
            .AddSingleton<IHttpContextAuthenticationVerifier, HttpContextAuthenticationVerifier>()
            .AddSingleton<IHttpContextResponseBuilder, HttpContextResponseBuilder>()
            .AddSingleton<IHttpContextAuthenticationTokenVerifier, HttpContextAuthenticationTokenVerifier>()
            .AddSingleton<IHttpContextAuthenticationIdentityProvider, DefaultHttpContextAuthenticationIdentityProvider>();
        return services;
    }
}
