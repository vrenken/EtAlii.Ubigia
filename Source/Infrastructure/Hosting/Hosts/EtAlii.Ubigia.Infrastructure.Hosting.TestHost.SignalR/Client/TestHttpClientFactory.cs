namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
	using System.Net;
	using System.Net.Http;
	using EtAlii.Ubigia.Api.Transport.Rest;
	using IHttpClientFactory = EtAlii.Ubigia.Api.Transport.Rest.IHttpClientFactory;
    using EtAlii.xTechnology.Threading;

	internal class TestHttpClientFactory : IHttpClientFactory
	{
		private readonly xTechnology.Hosting.IHostTestContext _testContext;
        private readonly IContextCorrelator _contextCorrelator;

        public TestHttpClientFactory(
            IHostTestContext testContext,
            IContextCorrelator contextCorrelator)
        {
            _testContext = testContext;
            _contextCorrelator = contextCorrelator;
        }

        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
	        var handler = _testContext.CreateHandler();
#pragma warning disable CA2000 // The HttpClient is instructed to dispose the handler.
			var client = new HttpClient(new TestHttpClientMessageHandler(handler, credentials, hostIdentifier, authenticationToken), true);
#pragma warning restore CA2000

	        // Set the Accept header for BSON.
	        client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType);

            // Apply all correlation ID's as http headers for the current request.
            foreach (var correlationId in Correlation.AllIds)
            {
                if (_contextCorrelator.TryGetValue(correlationId, out var correlationIdValue))
                {
                    client.DefaultRequestHeaders.Add(correlationId, correlationIdValue);
                }
            }

			return client;
        }
    }
}
