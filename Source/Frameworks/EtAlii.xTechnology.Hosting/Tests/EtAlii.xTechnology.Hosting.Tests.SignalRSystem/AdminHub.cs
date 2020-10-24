namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem
{
	using System;
	using Microsoft.AspNetCore.SignalR;

	public class AdminHub : Hub<IAdminClient>
    {
		public string GetFirst()
		{
			return $"{typeof(AdminHub)}_{Environment.TickCount}";
		}
        
		public string GetSecond(string postfix)
		{
			return $"{typeof(AdminHub)}_{postfix}";
		}
    }
}
