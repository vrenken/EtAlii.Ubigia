namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using Microsoft.AspNetCore.Http.Connections;
	using Microsoft.AspNetCore.SignalR.Client;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	public class HubConnectionFactory
    {
	    public HubConnection CreateForHost(ISignalRTransport transport, Uri address, string hostIdentifier)
	    {
		    var builder = new HubConnectionBuilder()
			    .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
			    .ConfigureLogging(options =>
			    {
				    if (Debugger.IsAttached)
				    {
					    options.AddDebug();
					    options.SetMinimumLevel(LogLevel.Trace);
				    }
			    })
				.WithUrl(address, options =>
			    {
				    options.HttpMessageHandlerFactory = (handler) => transport.HttpMessageHandlerFactory() ?? handler;
				    options.Transports = HttpTransportType.LongPolling;
					options.Headers = new Dictionary<string, string>() {{"Host-Identifier", hostIdentifier}};
				});		    
			return builder.Build();
		}

	    public HubConnection Create(ISignalRSpaceTransport transport, Uri address)
	    {
		    return Create(transport, address, transport.AuthenticationToken);
	    }
	    public HubConnection Create(ISignalRStorageTransport transport, Uri address)
	    {
		    return Create(transport, address, transport.AuthenticationToken);
	    }

		public HubConnection Create(ISignalRTransport transport, Uri address, string authenticationToken)
	    {
		    var builder = new HubConnectionBuilder()
			    .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
			    .ConfigureLogging(options =>
			    {
				    if (Debugger.IsAttached)
				    {
					    options.AddDebug();
					    options.SetMinimumLevel(LogLevel.Trace);
				    }
			    })
			    .WithUrl(address, options =>
			    {
				    options.HttpMessageHandlerFactory = (handler) => transport.HttpMessageHandlerFactory() ?? handler;
				    options.Transports = HttpTransportType.LongPolling;
					options.Headers = new Dictionary<string, string> {{"Authentication-Token", authenticationToken}};
				});
			return builder.Build();
        }
    }
}
