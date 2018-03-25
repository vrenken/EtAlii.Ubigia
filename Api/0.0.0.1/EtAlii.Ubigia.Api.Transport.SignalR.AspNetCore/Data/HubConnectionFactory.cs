namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System.Net.Http;
	using Microsoft.AspNetCore.SignalR;
	using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.Sockets;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
	    private IHubConnectionBuilder CreateBuilder(HttpMessageHandler httpClientHandler, string address)
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

	    public HubConnection CreateForHost(HttpMessageHandler httpClientHandler, string address, string hostIdentifier)
	    {
		    var builder = CreateBuilder(httpClientHandler, address);
		    builder = builder.WithHeader("Host-Identifier", hostIdentifier);
			return builder.Build();
		}

	    public HubConnection Create(ISignalRSpaceTransport transport, string address)
	    {
		    return Create(transport.HttpMessageHandler, address, transport.AuthenticationToken);
	    }
	    public HubConnection Create(ISignalRStorageTransport transport, string address)
	    {
		    return Create(transport.HttpMessageHandler, address, transport.AuthenticationToken);
	    }

		public HubConnection Create(HttpMessageHandler httpClientHandler, string address, string authenticationToken)
	    {
		    var builder = CreateBuilder(httpClientHandler, address);
			builder = builder.WithHeader("Authentication-Token", authenticationToken);
			return builder.Build();
        }
    }
}
