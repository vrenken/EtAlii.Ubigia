// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Rest
{
	using System;
	using System.Linq;
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

			IActionResult result;
			var authenticationToken = authenticationTokenConverter.FromHttpActionContext(context.HttpContext);
			if (authenticationToken != null)
			{
				result = new UnauthorizedResult(); // "Unauthorized account"
				try
				{
					var accountTask = accountRepository!.Get(authenticationToken.Name);
                    var account = accountTask.GetAwaiter().GetResult();
					if (account != null)
					{
						// Let's be a bit safe, if the requiredRole is not null we are going to check the roles collection for it.
						if (_roles.Any())
						{
							var hasOneRequiredRole = account.Roles.Any(role => _roles.Any(requiredRole => requiredRole == role));
							if (hasOneRequiredRole)
							{
								// All fine.
								result = null;
							}
						}
						else
						{
							// All fine.
							result = null;
						}
					}
				}
				catch (Exception)
				{
					result = new UnauthorizedResult(); // "Unauthorized account"
				}
			}
			else
			{
				result = new BadRequestResult(); // "Missing Authentication-Token"
			}

			context.Result = result;
		}
	}
}
