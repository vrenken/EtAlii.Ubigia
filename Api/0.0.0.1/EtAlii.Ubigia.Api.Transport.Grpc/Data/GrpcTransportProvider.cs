namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using global::Grpc.Core;

	public class GrpcTransportProvider : ITransportProvider
    {
        private readonly Func<Uri, Channel> _grpcChannelFactory;

	    private GrpcTransportProvider(Func<Uri, Channel> grpcChannelFactory)
	    {
		    _grpcChannelFactory = grpcChannelFactory;
	    }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
	        // We always want to use a new authenticationTokenProvider.
	        var authenticationTokenProvider = new AuthenticationTokenProvider();
	        return new GrpcSpaceTransport(address, _grpcChannelFactory, authenticationTokenProvider);
        }

	    public static GrpcTransportProvider Create(Func<Uri, Channel> channelFactory)
	    {
		    return new GrpcTransportProvider(channelFactory);
	    }

	    public static GrpcTransportProvider Create()
	    {
		    var channelFactory = new Func<Uri, Channel>((channelAddress) => new Channel(channelAddress.DnsSafeHost, channelAddress.Port, ChannelCredentials.Insecure));
		    return new GrpcTransportProvider(channelFactory);
	    }
    }
}