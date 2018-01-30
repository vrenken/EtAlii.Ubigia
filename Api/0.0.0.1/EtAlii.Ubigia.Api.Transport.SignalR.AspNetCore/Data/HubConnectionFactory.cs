namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.Sockets;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class HubConnectionFactory
    {
	    private IHubConnectionBuilder CreateBuilder(string address)
	    {
		    var builder = new HubConnectionBuilder()
			    .WithUrl(address)
			    .WithTransport(TransportType.WebSockets)
			    //.WithMessageHandler(httpClientHandler)
			    .WithConsoleLogger(LogLevel.Debug)
			    .WithJsonProtocol((JsonSerializer)new SerializerFactory().Create());
		    return builder;
	    }

	    public HubConnection CreateForHost(string address, string hostIdentifier)
	    {
		    var builder = CreateBuilder(address);
		    builder = builder.WithHeader("Host-Identifier", hostIdentifier);
			return builder.Build();
		}

	    public HubConnection Create(string address, ISignalRSpaceTransport transport)
	    {
		    return Create(address, transport.AuthenticationToken);
	    }
	    public HubConnection Create(string address, ISignalRStorageTransport transport)
	    {
		    return Create(address, transport.AuthenticationToken);
	    }

		public HubConnection Create(string address, string authenticationToken)
	    {
		    var builder = CreateBuilder(address);
			builder = builder.WithHeader("Authentication-Token", authenticationToken);
			return builder.Build();
        }
    }
}
