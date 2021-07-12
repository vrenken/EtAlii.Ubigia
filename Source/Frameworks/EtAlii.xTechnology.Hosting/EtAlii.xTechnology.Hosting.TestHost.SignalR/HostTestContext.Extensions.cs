// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.SignalR.Client;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	public static class HostTestContextExtensions
    {
        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4792:Configuring loggers is security-sensitive",
            Justification = "Safe to do so here.")]
	    public static async Task<HubConnection> CreateSignalRConnection(this IHostTestContext context, string address)
	    {
		    var connection = new HubConnectionBuilder()
			    .WithUrl(address, options =>
                {
#pragma warning disable CA1416
                    options.HttpMessageHandlerFactory = _ => context.CreateHandler();
#pragma warning restore CA1416
                })
			    .AddJsonProtocol()
			    .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
			    .ConfigureLogging(options =>
			    {
                    // SonarQube: Make sure that this logger's configuration is safe.
                    // I think it is as this host is for testing only.
                    if (Debugger.IsAttached)
                    {
                        // This will set ALL logging to Debug level
                        options.AddDebug();
                        options.SetMinimumLevel(LogLevel.Trace);
                    }
			    })
			    .Build();

		    await connection.StartAsync().ConfigureAwait(false);
		    return connection;
	    }
    }
}
