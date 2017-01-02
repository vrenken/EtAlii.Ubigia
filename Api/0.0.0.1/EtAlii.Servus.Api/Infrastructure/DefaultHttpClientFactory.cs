namespace EtAlii.Servus.Api
{
    using System.Net;
    using System.Net.Http;

    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken)
        {
            var client = new HttpClient(new ClientHttpMessageHandler(credentials, hostIdentifier, authenticationToken));
            return client;
        }
    }
}
