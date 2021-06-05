namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

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

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Host-Identifier", _hostIdentifier);
            request.Headers.Add("Authentication-Token", _authenticationToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
