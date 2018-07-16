namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;
	using global::Grpc.Core;

	public class GrpcStorageTransportProvider : IStorageTransportProvider
    {
        private readonly Func<Uri, Channel> _grpcChannelFactory;

	    private readonly AuthenticationTokenProvider _storageAuthenticationTokenProvider;
	    
		public GrpcStorageTransportProvider(Func<Uri, Channel> grpcChannelFactory)
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
    }
}