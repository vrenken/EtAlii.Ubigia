// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem
{
	using System;
	using Microsoft.AspNetCore.SignalR;

	public class UserHub : Hub<IUserClient>
	{
		public string GetFirst()
		{
			return $"{typeof(UserHub)}_{Environment.TickCount}";
		}
        
		public string GetSecond(string postfix)
		{
			return $"{typeof(UserHub)}_{postfix}";
		}
    }
}
