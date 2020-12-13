﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Rest
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.Extensions.DependencyInjection;
    
    public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddAttributeBasedInfrastructureAuthorization(this IServiceCollection services, IInfrastructure infrastructure)
		{
			services
				.TryAddSingleton(infrastructure.Accounts)
				.TryAddSingleton(infrastructure.Storages)
				.TryAddSingleton(infrastructure.Configuration)
				.AddSingleton<IAuthenticationTokenConverter, AuthenticationTokenConverter>()
				.AddSingleton<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>()
				.AddSingleton<IHttpContextAuthenticationVerifier, HttpContextAuthenticationVerifier>()
				.AddSingleton<IHttpContextResponseBuilder, HttpContextResponseBuilder>()
				.AddSingleton<IHttpContextAuthenticationTokenVerifier, HttpContextAuthenticationTokenVerifier>()
				.AddSingleton<IHttpContextAuthenticationIdentityProvider, DefaultHttpContextAuthenticationIdentityProvider>();
			return services;
		}
	}
}