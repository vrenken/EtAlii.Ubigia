namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using global::Grpc.Core;

	public class GrpcTransportProvider : ITransportProvider
    {
        private readonly Func<Uri, Channel> _grpcChannelFactory;

	    public GrpcTransportProvider(Func<Uri, Channel> grpcChannelFactory)
	    {
		    _grpcChannelFactory = grpcChannelFactory;
	    }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
	        // We always want to use a new authenticationTokenProvider.
	        var authenticationTokenProvider = new AuthenticationTokenProvider();
	        return new GrpcSpaceTransport(address, _grpcChannelFactory, authenticationTokenProvider);
        }
    }
}