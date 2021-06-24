// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.Extensions.DependencyInjection;
    
    public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddSignalRInfrastructureAuthentication(this IServiceCollection services, IInfrastructure infrastructure)
	    {
		    services
			    .TryAddSingleton(infrastructure.Accounts)
			    .TryAddSingleton(infrastructure.Storages)
				.TryAddSingleton(infrastructure.Configuration)
			    .AddSingleton<IAuthenticationTokenConverter, AuthenticationTokenConverter>()
			    .AddSingleton<ISimpleAuthenticationVerifier, SimpleAuthenticationVerifier>()
			    .AddSingleton<ISimpleAuthenticationBuilder, SimpleAuthenticationBuilder>()
			    .AddSingleton<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>();
			return services;
	    }
	}
}