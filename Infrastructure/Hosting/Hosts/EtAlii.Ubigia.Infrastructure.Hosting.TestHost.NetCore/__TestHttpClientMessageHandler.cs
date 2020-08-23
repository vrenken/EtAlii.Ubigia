namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TestHttpClientMessageHandlerOld : DelegatingHandler
    {
        private readonly ICredentials _credentials;
        private readonly string _hostIdentifier;
        private readonly string _authenticationToken;

        public TestHttpClientMessageHandlerOld(
            HttpMessageHandler handler,
            ICredentials credentials, 
            string hostIdentifier, 
            string authenticationToken)
            : base(handler)
        {
            _credentials = credentials;
            _hostIdentifier = hostIdentifier; 
            _authenticationToken = authenticationToken;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_credentials != null)
            {
                var credentials = _credentials.GetCredential(request.RequestUri, "Basic-Authentication");

                
                request.Headers.Add("Test-UserName", credentials.UserName);
                request.Headers.Add("Test-Password", credentials.Password);
            }

            request.Headers.Add("Host-Identifier", _hostIdentifier);
            request.Headers.Add("Authentication-Token", _authenticationToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
