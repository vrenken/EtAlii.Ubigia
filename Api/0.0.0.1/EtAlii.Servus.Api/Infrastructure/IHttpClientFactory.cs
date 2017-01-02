namespace EtAlii.Servus.Api
{
    using System.Net;
    using System.Net.Http;

    public interface IHttpClientFactory
    {
        HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken);
    }
}
