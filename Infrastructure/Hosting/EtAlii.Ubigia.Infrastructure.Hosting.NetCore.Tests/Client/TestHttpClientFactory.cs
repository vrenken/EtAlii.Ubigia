namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System.Net;
	using System.Net.Http;
	using EtAlii.Ubigia.Api.Transport.WebApi;
	using IHttpClientFactory = EtAlii.Ubigia.Api.Transport.WebApi.IHttpClientFactory;

	internal class TestHttpClientFactory : IHttpClientFactory
	{
		private readonly xTechnology.Hosting.IHostTestContext _testContext;

        public TestHttpClientFactory(xTechnology.Hosting.IHostTestContext testContext)
        {
	        _testContext = testContext;
        }

        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
	        var handler = _testContext.CreateHandler();
			var client = new HttpClient(new TestHttpClientMessageHandler(handler, credentials, hostIdentifier, authenticationToken));

	        // Set the Accept header for BSON.
	        client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType);

			return client;
        }
    }
}