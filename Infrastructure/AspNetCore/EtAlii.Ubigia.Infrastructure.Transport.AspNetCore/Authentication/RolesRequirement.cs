namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
	using System;
    using System.Net;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;


	internal class RolesRequirement : IAuthorizationRequirement
	{
		public readonly string[] RequiredRoles;

		public RolesRequirement(string[] requiredRoles)
		{
			RequiredRoles = requiredRoles;
		}
	}
}