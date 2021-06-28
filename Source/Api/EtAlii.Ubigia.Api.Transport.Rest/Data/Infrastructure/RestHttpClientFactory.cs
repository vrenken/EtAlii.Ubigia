// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System;
    using System.Net;
	using System.Net.Http;
    using EtAlii.xTechnology.Threading;

    public class RestHttpClientFactory : IHttpClientFactory
    {
        private readonly IContextCorrelator _contextCorrelator;

        public RestHttpClientFactory(IContextCorrelator contextCorrelator)
        {
            _contextCorrelator = contextCorrelator;
        }

        public HttpClient Create(ICredentials credentials, string hostIdentifier, string authenticationToken, Uri address)
        {
#pragma warning disable CA2000 // The HttpClient is instructed to dispose the handler.
            var client = new HttpClient(new ClientHttpMessageHandler(credentials, hostIdentifier, authenticationToken), true);
#pragma warning restore CA2000

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
