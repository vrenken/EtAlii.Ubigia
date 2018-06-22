namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using Microsoft.AspNetCore.Http.Connections;
	using Microsoft.AspNetCore.SignalR.Client;
	using Microsoft.Extensions.DependencyInjection;

	public class HubConnectionFactory
    {
	    public HubConnection CreateForHost(HttpMessageHandler httpClientHandler, Uri address, string hostIdentifier)
	    {
		    var builder = new HubConnectionBuilder()
		        .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
				.WithUrl(address, options =>
			    {
				    options.HttpMessageHandlerFactory = (handler) => httpClientHandler ?? handler;
					options.Transports = HttpTransportType.LongPolling;
					options.Headers = new Dictionary<string, string>() {{"Host-Identifier", hostIdentifier}};
				});		    
			return builder.Build();
		}

	    public HubConnection Create(ISignalRSpaceTransport transport, Uri address)
	    {
		    return Create(transport.HttpMessageHandler, address, transport.AuthenticationToken);
	    }
	    public HubConnection Create(ISignalRStorageTransport transport, Uri address)
	    {
		    return Create(transport.HttpMessageHandler, address, transport.AuthenticationToken);
	    }

		public HubConnection Create(HttpMessageHandler httpClientHandler, Uri address, string authenticationToken)
	    {
		    var builder = new HubConnectionBuilder()
			    .AddJsonProtocol(options => SerializerFactory.Configure(options.PayloadSerializerSettings))
				.WithUrl(address, options =>
			    {
				    options.HttpMessageHandlerFactory = (handler) => httpClientHandler ?? handler;
					options.Transports = HttpTransportType.LongPolling;
					options.Headers = new Dictionary<string, string>() {{"Authentication-Token", authenticationToken}};
				});
			return builder.Build();
        }
    }
}
