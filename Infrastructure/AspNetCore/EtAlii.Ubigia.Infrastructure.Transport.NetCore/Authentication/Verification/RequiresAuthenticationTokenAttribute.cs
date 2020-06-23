namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
	using System;
	using System.Linq;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.DependencyInjection;

	[AttributeUsage(AttributeTargets.Class)]
	public class RequiresAuthenticationTokenAttribute : Attribute, IAuthorizationFilter
	{
		private readonly string[] _roles;
		public RequiresAuthenticationTokenAttribute(string role)
		{
			_roles = new[] {role, Role.System};
		}
		
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			// I'm not too happy that we aren't using DI here, but as the REST protocol probably won't live that long 
			// It is rather low priority compared to gRPC and SignalR.
			var authenticationTokenConverter = context.HttpContext.RequestServices.GetService<IAuthenticationTokenConverter>();
			var accountRepository = context.HttpContext.RequestServices.GetService<IAccountRepository>();
			var authenticationToken = authenticationTokenConverter.FromHttpActionContext(context.HttpContext);
			if (authenticationToken != null)
			{
				try
				{
					var account = accountRepository.Get(authenticationToken.Name);
					if (account != null)
					{
						// Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
						if (_roles.Any())
						{
							var hasOneRequiredRole = account.Roles.Any(role => _roles.Any(requiredRole => requiredRole == role));
							if (hasOneRequiredRole)
							{
								// All fine.
								return;
							}
						}
						else
						{
							// All fine.
							return;
						}
					}
				}
				catch (Exception)
				{
					context.Result = new UnauthorizedResult(); // "Unauthorized account" 
					return;
				}
			}
			else
			{
				context.Result = new UnauthorizedResult(); // "Missing Authentication-Token" 
				return;
			}
			context.Result = new UnauthorizedResult(); // "Unauthorized account" 
		}
	}
}