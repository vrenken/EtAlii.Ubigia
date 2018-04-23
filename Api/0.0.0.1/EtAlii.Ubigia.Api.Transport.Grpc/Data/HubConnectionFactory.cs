namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using System.Net.Http;
    using Microsoft.AspNetCore.Sockets;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
	    private IHubConnectionBuilder CreateBuilder(HttpMessageHandler httpClientHandler, Uri address)
	    {
		    var builder = new HubConnectionBuilder()
			    .WithUrl(address)
			    //.WithTransport(TransportType.WebSockets) // TODO: Activate websockets based testing when it becomes supported. 
			    .WithTransport(TransportType.LongPolling)
			    .WithConsoleLogger(LogLevel.Debug)
			    .WithJsonProtocol(new JsonHubProtocolOptions { PayloadSerializerSettings = SerializerFactory.CreateSerializerSettings() });
		    if (httpClientHandler != null)
		    {
			    builder = builder.WithMessageHandler(httpClientHandler);
		    }
			return builder;
	    }

	    public HubConnection CreateForHost(HttpMessageHandler httpClientHandler, Uri address, string hostIdentifier)
	    {
		    var builder = CreateBuilder(httpClientHandler, address);
		    builder = builder.WithHeader("Host-Identifier", hostIdentifier);
			return builder.Build();
		}

	    public HubConnection Create(IGrpcSpaceTransport transport, Uri address)
	    {
		    return Create(transport.HttpMessageHandler, address, transport.AuthenticationToken);
	    }
	    public HubConnection Create(IGrpcStorageTransport transport, Uri address)
	    {
		    return Create(transport.HttpMessageHandler, address, transport.AuthenticationToken);
	    }

		public HubConnection Create(HttpMessageHandler httpClientHandler, Uri address, string authenticationToken)
	    {
		    var builder = CreateBuilder(httpClientHandler, address);
			builder = builder.WithHeader("Authentication-Token", authenticationToken);
			return builder.Build();
        }
    }
}
