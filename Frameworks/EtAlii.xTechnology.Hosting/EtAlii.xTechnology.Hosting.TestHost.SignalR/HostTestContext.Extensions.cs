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
	    [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
	    public static async Task<HubConnection> CreateSignalRConnection(this IHostTestContext context, string address)
	    {
		    var connection = new HubConnectionBuilder()
			    .WithUrl(address)//, HttpTransportType.WebSockets, options => options.SkipNegotiation = true)
			    .AddJsonProtocol()
			    .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
			    .ConfigureLogging(logging =>
			    {
				    if (!Debugger.IsAttached) return;
				    
				    // SonarQube: Make sure that this logger's configuration is safe.
				    // I think it is as this host is for testing only.
				    logging.AddDebug();

				    // This will set ALL logging to Debug level
				    logging.SetMinimumLevel(LogLevel.Trace);
			    })
			    .Build();

		    await connection.StartAsync();
		    return connection;
	    }
    }
}
