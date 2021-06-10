namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.Ubigia.Serialization;
	using Microsoft.AspNetCore.SignalR.Client;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	public class HubConnectionFactory
    {
        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
	    public HubConnection CreateForHost(ISignalRTransport transport, Uri address, string hostIdentifier)
	    {
		    var builder = new HubConnectionBuilder()
			    .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
			    .ConfigureLogging(options =>
			    {
                    // SonarQube: Make sure that this logger's configuration is safe.
                    // As we only add the logging services this ought to be safe. It is when and how they are configured that matters.
				    if (Debugger.IsAttached)
				    {
					    options.AddDebug();
					    options.SetMinimumLevel(LogLevel.Trace);
				    }
			    })
				.WithUrl(address, options =>
			    {
				    options.HttpMessageHandlerFactory = handler => transport.HttpMessageHandlerFactory() ?? handler;
					options.Headers = new Dictionary<string, string> {{"Host-Identifier", hostIdentifier}};
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

        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
		public HubConnection Create(ISignalRTransport transport, Uri address, string authenticationToken)
	    {
		    var builder = new HubConnectionBuilder()
			    .AddNewtonsoftJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
			    .ConfigureLogging(options =>
			    {
                    // SonarQube: Make sure that this logger's configuration is safe.
                    // As we only add the logging services this ought to be safe. It is when and how they are configured that matters.
				    if (Debugger.IsAttached)
				    {
					    options.AddDebug();
					    options.SetMinimumLevel(LogLevel.Trace);
				    }
			    })
			    .WithUrl(address, options =>
			    {
				    options.HttpMessageHandlerFactory = handler => transport.HttpMessageHandlerFactory() ?? handler;
					options.Headers = new Dictionary<string, string> {{"Authentication-Token", authenticationToken}};
				});
			return builder.Build();
        }
    }
}
