namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using global::Grpc.Core;

	public class GrpcTransportProvider : ITransportProvider
    {
        private string _authenticationToken;
	    private readonly Func<Uri, Channel> _grpcChannelFactory;

	    public GrpcTransportProvider(Func<Uri, Channel> grpcChannelFactory)
	    {
		    _grpcChannelFactory = grpcChannelFactory;
	    }

		public static GrpcTransportProvider Create(Func<Uri, Channel> grpcChannelFactory = null)
        { 
	        return new GrpcTransportProvider(grpcChannelFactory);//new ClientHttpMessageHandler());
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new GrpcSpaceTransport(
	            address,
	            _grpcChannelFactory,
				v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}