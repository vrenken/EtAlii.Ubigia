namespace EtAlii.Ubigia.Api.Transport.WebApi
{
	using System.Net;
	using System.Net.Http;

	// TODO: Should be made internal
    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
#pragma warning disable CA2000 // The HttpClient is instructed to dispose the handler.            
            var client = new HttpClient(new ClientHttpMessageHandler(credentials, hostIdentifier, authenticationToken), true);
#pragma warning restore CA2000

			// Set the Accept header for BSON.
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType);

			return client;
        }
    }
}
