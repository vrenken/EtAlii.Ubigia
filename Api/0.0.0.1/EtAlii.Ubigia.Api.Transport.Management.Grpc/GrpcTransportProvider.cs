namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using global::Grpc.Core;
	using global::Grpc.Net.Client;

	public class GrpcStorageTransportProvider : IStorageTransportProvider
    {
        private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;

	    private readonly AuthenticationTokenProvider _storageAuthenticationTokenProvider;
	    
		private GrpcStorageTransportProvider(Func<Uri, GrpcChannel> grpcChannelFactory)
		{
			_grpcChannelFactory = grpcChannelFactory;
			_storageAuthenticationTokenProvider = new AuthenticationTokenProvider();
		}

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
	        // We always use a new authenticationTokenProvider for space based access.
	        var authenticationTokenProvider = new AuthenticationTokenProvider { AuthenticationToken = _storageAuthenticationTokenProvider.AuthenticationToken };
            return new GrpcSpaceTransport(address, _grpcChannelFactory, authenticationTokenProvider);
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
	        // We always want to use the same authenticationTokenProvider for storage based access.
	        var authenticationTokenProvider = _storageAuthenticationTokenProvider;
            return new GrpcStorageTransport(address, _grpcChannelFactory, authenticationTokenProvider);
        }
	    
	    
	    public static GrpcStorageTransportProvider Create(Func<Uri, GrpcChannel> channelFactory)
	    {
		    return new GrpcStorageTransportProvider(channelFactory);
	    }

	    public static GrpcStorageTransportProvider Create()
	    {
		    var channelFactory = new Func<Uri, GrpcChannel>((channelAddress) =>
		    {
			    var options = new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure };
			    return  GrpcChannel.ForAddress(channelAddress, options);
		    });
		    return new GrpcStorageTransportProvider(channelFactory);
	    }
    }
}