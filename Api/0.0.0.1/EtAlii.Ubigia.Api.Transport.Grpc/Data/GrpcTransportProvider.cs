namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using global::Grpc.Core;
	using global::Grpc.Net.Client;

	public class GrpcTransportProvider : ITransportProvider
    {
        private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;

	    private GrpcTransportProvider(Func<Uri, GrpcChannel> grpcChannelFactory)
	    {
		    _grpcChannelFactory = grpcChannelFactory;
	    }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
	        // We always want to use a new authenticationTokenProvider.
	        var authenticationTokenProvider = new AuthenticationTokenProvider();
	        return new GrpcSpaceTransport(address, _grpcChannelFactory, authenticationTokenProvider);
        }

	    public static GrpcTransportProvider Create(Func<Uri, GrpcChannel> channelFactory)
	    {
		    return new GrpcTransportProvider(channelFactory);
	    }

	    public static GrpcTransportProvider Create()
	    {
		    var channelFactory = new Func<Uri, GrpcChannel>((channelAddress) =>
		    {
			    var options = new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure };
			    return GrpcChannel.ForAddress(channelAddress, options);
		    });
		    return new GrpcTransportProvider(channelFactory);
	    }
    }
}