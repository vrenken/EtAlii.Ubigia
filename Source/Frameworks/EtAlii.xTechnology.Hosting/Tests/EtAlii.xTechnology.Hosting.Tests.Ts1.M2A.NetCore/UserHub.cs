// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
