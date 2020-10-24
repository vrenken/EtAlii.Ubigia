namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Net.Http;
	using Grpc.Core;
	using Grpc.Net.Client;

	public static class HostTestContextExtensions
    {
	    static HostTestContextExtensions()
	    {
		    //These switches must be set before creating the GrpcChannel/HttpClient.
		    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
		    // https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-insecure-grpc-services-with-net-core-client
		    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
	    }
	    public static TGrpcClient CreateClient<TGrpcClient>(this IHostTestContext context, string address, Func<GrpcChannel, TGrpcClient> construct)
		    where TGrpcClient: ClientBase
	    {
		    var channel = CreateChannel(context, address);
		    return construct(channel);
	    }

	    public static GrpcChannel CreateChannel(this IHostTestContext context, Uri address) => CreateChannel(context, address.ToString());

	    public static GrpcChannel CreateChannel(this IHostTestContext context, string address)
	    {
		    var httpClient = new HttpClient();
			
		    var options = new GrpcChannelOptions
		    {
			    HttpClient = httpClient, 
			    Credentials = ChannelCredentials.Insecure,
			    DisposeHttpClient = true,
		    };
		    var channel = GrpcChannel.ForAddress(address, options);
		    return channel;	
	    }
    }
}
