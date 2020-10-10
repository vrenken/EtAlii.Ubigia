namespace EtAlii.xTechnology.Hosting
{
	using System.Net.Http;

	public static class HostTestContextExtensions
    {
	    public static HttpClient CreateClient(this IHostTestContext context)
	    {
		    // dynamic kestrelServerOptions = context.Host.Server.Options
		    // var listenOptions = (List<ListenOptions>)kestrelServerOptions.ListenOptions
		    
		    //var handler = new ClientHandler()
		    //context.Host.Server.Options.
		    return new HttpClient();
		    // // var handler = new HttpClientHandler()
		    // var handler = context.Host.Server.CreateHandler()
		    // return new HttpClient(handler,true)
	    }
    }
}
