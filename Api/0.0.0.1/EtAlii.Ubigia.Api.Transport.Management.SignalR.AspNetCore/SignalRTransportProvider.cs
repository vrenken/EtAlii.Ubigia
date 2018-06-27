namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
	using System;
	using System.Net.Http;
	using EtAlii.Ubigia.Api.Transport;
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
	        return new SignalRStorageTransportProvider(httpMessageHandlerFactory);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
	            _httpMessageHandlerFactory,
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new SignalRStorageTransport(
				_httpMessageHandlerFactory,
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}