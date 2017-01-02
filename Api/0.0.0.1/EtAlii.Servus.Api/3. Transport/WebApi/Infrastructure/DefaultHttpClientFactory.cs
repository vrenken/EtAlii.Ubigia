namespace EtAlii.Servus.Api.Transport
{
    using System.Net;
    using System.Net.Http;

    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
            var client = new HttpClient(new ClientHttpMessageHandler(credentials, hostIdentifier, authenticationToken));

            // Set the Accept header for BSON.
            //client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(PayloadMediaTypeFormatter.MediaType);
            
            return client;
        }
    }
}
