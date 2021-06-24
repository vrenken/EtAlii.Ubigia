// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest
{
    using System;
    using System.Net;
	using System.Net.Http;
    using System.Text;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using IHttpClientFactory = EtAlii.Ubigia.Api.Transport.Rest.IHttpClientFactory;
    using EtAlii.xTechnology.Threading;

	internal class TestHttpClientFactory : IHttpClientFactory
	{
		private readonly RestHostTestContext _testContext;
        private readonly IContextCorrelator _contextCorrelator;

        public TestHttpClientFactory(
            RestHostTestContext testContext,
            IContextCorrelator contextCorrelator)
        {
            _testContext = testContext;
            _contextCorrelator = contextCorrelator;
        }

        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken, Uri address)
        {
            var client  = _testContext.CreateClient();

            if (credentials != null)
            {
                var crdntls = credentials.GetCredential(address, "Basic-Authentication");
                var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(crdntls!.UserName + ":" + crdntls.Password));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
            }

            client.DefaultRequestHeaders.Add("Host-Identifier", hostIdentifier);
            client.DefaultRequestHeaders.Add("Authentication-Token", authenticationToken);

	        // Set the Accept header for BSON.
	        client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(MediaType.Bson);

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
