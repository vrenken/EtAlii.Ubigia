namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using System.Net.Http;

	public class GrpcTransportProvider : ITransportProvider
    {
        private string _authenticationToken;
	    private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

	    public GrpcTransportProvider(Func<HttpMessageHandler> httpMessageHandlerFactory)
	    {
		    _httpMessageHandlerFactory = httpMessageHandlerFactory;
	    }

		public static GrpcTransportProvider Create(Func<HttpMessageHandler> httpMessageHandlerFactory = null)
        {
	        return new GrpcTransportProvider(httpMessageHandlerFactory);//new ClientHttpMessageHandler());
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new GrpcSpaceTransport(
	            _httpMessageHandlerFactory?.Invoke(),
				v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}