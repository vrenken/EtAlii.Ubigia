namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
	using System;
	using Microsoft.AspNetCore.SignalR;

	public class UserHub : Hub
    {
		public string GetSimple()
		{
			return $"{nameof(UserHub)}_{Environment.TickCount}";
		}
        
		public string GetComplex(string postfix)
		{
			return $"{nameof(UserHub)}_{postfix}";
		}
    }
}
