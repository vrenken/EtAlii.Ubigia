// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System;
	using System.Net.Http;

	public class SignalRTransportProvider : ITransportProvider
    {
        private string _authenticationToken;
	    private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

        private SignalRTransportProvider(Func<HttpMessageHandler> httpMessageHandlerFactory)
	    {
		    _httpMessageHandlerFactory = httpMessageHandlerFactory;
	    }

		public static SignalRTransportProvider Create(Func<HttpMessageHandler> httpMessageHandlerFactory = null)
        {
	        return new(httpMessageHandlerFactory);//new ClientHttpMessageHandler())
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new SignalRSpaceTransport(
	            address,
	            _httpMessageHandlerFactory,
				v => _authenticationToken = v,
                () => _authenticationToken);
        }
    }
}
