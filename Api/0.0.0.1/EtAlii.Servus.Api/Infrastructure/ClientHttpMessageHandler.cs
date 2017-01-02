namespace EtAlii.Servus.Api
{
    using System.Net;
    using System.Net.Http;

    internal class ClientHttpMessageHandler : HttpClientHandler
    {
        private readonly string _hostIdentifier;
        private readonly string _authenticationToken;

        public ClientHttpMessageHandler(
            ICredentials credentials, 
            string hostIdentifier, 
            string authenticationToken)
        {
            Credentials = credentials;
            UseDefaultCredentials = Credentials == null;
            _hostIdentifier = hostIdentifier;
            _authenticationToken = authenticationToken;
            
            AllowAutoRedirect = true;
            UseProxy = true;
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            request.Headers.Add("Host-Identifier", _hostIdentifier);
            request.Headers.Add("Authentication-Token", _authenticationToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
