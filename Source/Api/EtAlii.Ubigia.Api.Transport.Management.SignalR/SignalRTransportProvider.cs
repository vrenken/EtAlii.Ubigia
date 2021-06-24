// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
	using System;
	using System.Net.Http;
	using EtAlii.Ubigia.Api.Transport.SignalR;

	public class SignalRStorageTransportProvider : IStorageTransportProvider
    {
        private string _authenticationToken;
	    private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

		public SignalRStorageTransportProvider(Func<HttpMessageHandler> httpMessageHandlerFactory)
		{
			_httpMessageHandlerFactory = httpMessageHandlerFactory;
		}

		public static SignalRStorageTransportProvider Create(Func<HttpMessageHandler> httpMessageHandlerFactory = null)
        {
	        return new(httpMessageHandlerFactory);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new SignalRSpaceTransport(
	            address,
	            _httpMessageHandlerFactory,
                v => _authenticationToken = v,
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new SignalRStorageTransport(
	            address,
				_httpMessageHandlerFactory,
                v => _authenticationToken = v,
                () => _authenticationToken);
        }
    }
}
