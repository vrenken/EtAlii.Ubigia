namespace EtAlii.Ubigia.Api.Transport.Rest
{
	using System.Net;
	using System.Net.Http;
    using EtAlii.xTechnology.Threading;

    internal class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly IContextCorrelator _contextCorrelator;

        public DefaultHttpClientFactory(IContextCorrelator contextCorrelator)
        {
            _contextCorrelator = contextCorrelator;
        }

        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
#pragma warning disable CA2000 // The HttpClient is instructed to dispose the handler.
            var client = new HttpClient(new ClientHttpMessageHandler(credentials, hostIdentifier, authenticationToken), true);
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
