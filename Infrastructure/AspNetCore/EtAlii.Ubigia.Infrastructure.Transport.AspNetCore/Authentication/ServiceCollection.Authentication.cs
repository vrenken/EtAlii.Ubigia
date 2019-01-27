namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.IdentityModel.Tokens;

	public static partial class ServiceCollectionExtensions
	{
		private static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
//		private static readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();

		public static IServiceCollection AddInfrastructureHttpContextAuthentication(this IServiceCollection services, IInfrastructure infrastructure)
		{
			services
				.TryAddSingleton<IAccountRepository>(infrastructure.Accounts)
				.TryAddSingleton<IStorageRepository>(infrastructure.Storages)
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
			    .TryAddSingleton<IAccountRepository>(infrastructure.Accounts)
			    .TryAddSingleton<IStorageRepository>(infrastructure.Storages)
				.TryAddSingleton<IInfrastructureConfiguration>(infrastructure.Configuration)
			    .AddSingleton<IAuthenticationTokenConverter, AuthenticationTokenConverter>()
			    .AddSingleton<ISimpleAuthenticationVerifier, SimpleAuthenticationVerifier>()
			    .AddSingleton<ISimpleAuthenticationBuilder, SimpleAuthenticationBuilder>()
			    .AddSingleton<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>();

			return services;
	    }

//		private static void AddJwtBearer(IServiceCollection services)
//		{
//			// Source: https://github.com/aspnet/SignalR/blob/dev/samples/JwtSample/Startup.cs#L36
//			services.AddAuthorization(options =>
//			{
//				options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
//				{
//					policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
//					policy.RequireClaim(ClaimTypes.NameIdentifier);
//				});
//			});
//			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//				.AddJwtBearer(options =>
//				{
//					options.TokenValidationParameters =
//						new TokenValidationParameters
//						{
//							LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
//							ValidateAudience = false,
//							ValidateIssuer = false,
//							ValidateActor = false,
//							ValidateLifetime = true,
//							IssuerSigningKey = SecurityKey
//						};
//					options.Events = new JwtBearerEvents
//					{
//						OnMessageReceived = context =>
//						{
//							var accessToken = context.Request.Query["access_token"];
//							if (!string.IsNullOrEmpty(accessToken) &&
//							    (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
//							{
//								context.Token = context.Request.Query["access_token"];
//							}
//							return Task.CompletedTask;
//						}
//					};
//				});
//		}


		//.AddAuthentication(options =>
		//{
		//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		//})
		//.AddJwtBearer(options =>
		//{
		//    options.TokenValidationParameters = new TokenValidationParameters
		//    {
		//        ValidateIssuer = true,
		//        ValidateAudience = true,
		//        ValidateIssuerSigningKey = true,
		//        ValidIssuer = _configuration["Jwt:Issuer"],
		//        ValidAudience = _configuration["Jwt:Issuer"],
		//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
		//        ValidateLifetime = true, //validate the expiration and not before values in the token
		//        ClockSkew = TimeSpan.FromMinutes(1) //5 minute tolerance for the expiration date

		//    };
		//});

	}
}