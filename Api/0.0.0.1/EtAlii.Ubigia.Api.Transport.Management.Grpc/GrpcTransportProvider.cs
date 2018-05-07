namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using System.Net.Http;
	using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;

    public class GrpcStorageTransportProvider : IStorageTransportProvider
    {
        private string _authenticationToken;
	    private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

		public GrpcStorageTransportProvider(Func<HttpMessageHandler> httpMessageHandlerFactory)
		{
			_httpMessageHandlerFactory = httpMessageHandlerFactory;
		}

		public static GrpcStorageTransportProvider Create(Func<HttpMessageHandler> httpMessageHandlerFactory = null)
        {
	        return new GrpcStorageTransportProvider(httpMessageHandlerFactory);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new GrpcSpaceTransport(
	            _httpMessageHandlerFactory?.Invoke(),
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new GrpcStorageTransport(
				_httpMessageHandlerFactory?.Invoke(),
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}