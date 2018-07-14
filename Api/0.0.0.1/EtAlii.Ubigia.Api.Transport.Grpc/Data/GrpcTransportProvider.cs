namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using global::Grpc.Core;

	public class GrpcTransportProvider : ITransportProvider
    {
        private readonly Func<Uri, Channel> _grpcChannelFactory;

	    private readonly IAuthenticationTokenProvider _authenticationTokenProvider;
	    
	    public GrpcTransportProvider(Func<Uri, Channel> grpcChannelFactory)
	    {
		    _grpcChannelFactory = grpcChannelFactory;
		    _authenticationTokenProvider = new AuthenticationTokenProvider();
	    }

		public static GrpcTransportProvider Create(Func<Uri, Channel> grpcChannelFactory = null)
        { 
	        return new GrpcTransportProvider(grpcChannelFactory);//new ClientHttpMessageHandler());
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new GrpcSpaceTransport(address, _grpcChannelFactory, _authenticationTokenProvider);
        }
    }
}