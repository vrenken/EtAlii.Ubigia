namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TestHttpClientMessageHandler : DelegatingHandler
    {
        private readonly ICredentials _credentials;
        private readonly string _hostIdentifier;
        private readonly string _authenticationToken;

        public TestHttpClientMessageHandler(
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
	            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(credentials.UserName + ":" + credentials.Password));
	            request.Headers.Add("Authorization", "Basic " + encoded);
                //request.Headers.Add("Test-UserName", credentials.UserName)
                //request.Headers.Add("Test-Password", credentials.Password)
            }

            request.Headers.Add("Host-Identifier", _hostIdentifier);
            request.Headers.Add("Authentication-Token", _authenticationToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
