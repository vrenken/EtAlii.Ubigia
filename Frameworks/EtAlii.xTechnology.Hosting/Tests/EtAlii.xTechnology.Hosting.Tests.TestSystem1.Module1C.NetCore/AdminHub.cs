namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Api.NetCore
{
	using System;
	using Microsoft.AspNetCore.SignalR;

	public class AdminHub : Hub
    {
		public string GetSimple()
		{
			return $"{nameof(AdminHub)}_{Environment.TickCount}";
		}
        
		public string GetComplex(string postfix)
		{
			return $"{nameof(AdminHub)}_{postfix}";
		}
    }
}
