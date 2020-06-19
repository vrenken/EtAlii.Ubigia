namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.Extensions.DependencyInjection;

	public static partial class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructureHttpContextAuthentication(this IServiceCollection services, IInfrastructure infrastructure)
		{
			services
				.TryAddSingleton(infrastructure.Accounts)
				.TryAddSingleton(infrastructure.Storages)
				.AddSingleton<IAuthenticationTokenConverter, AuthenticationTokenConverter>()
				.AddSingleton<IHttpContextAuthenticationVerifier, HttpContextAuthenticationVerifier>()
				.AddSingleton<IHttpContextResponseBuilder, HttpContextResponseBuilder>()
				.AddSingleton<IHttpContextAuthenticationTokenVerifier, HttpContextAuthenticationTokenVerifier>()
				.AddSingleton<IHttpContextAuthenticationIdentityProvider, DefaultHttpContextAuthenticationIdentityProvider>();
			return services;
		}

		public static IServiceCollection AddInfrastructureSimpleAuthentication(this IServiceCollection services, IInfrastructure infrastructure)
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