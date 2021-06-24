// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem
{
	using System.Diagnostics;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class AdminService : ServiceBase
	{
		public AdminService(IConfigurationSection configuration)
			: base(configuration)
		{
		}

		protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder
				.UseRouting()
				.UseCors(builder =>
				{
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins($"http://{HostString}");
				})
				.UseEndpoints(endpoints => endpoints.MapHub<AdminHub>(SignalRHub.Admin));
		}

		protected override void ConfigureServices(IServiceCollection services)
		{
			services
				.AddCors()
				.AddSignalR(options => options.EnableDetailedErrors = Debugger.IsAttached);
		}
	}
}
