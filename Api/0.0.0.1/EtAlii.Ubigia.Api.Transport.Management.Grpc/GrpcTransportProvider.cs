namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;
	using global::Grpc.Core;

	public class GrpcStorageTransportProvider : IStorageTransportProvider
    {
        private string _authenticationToken;
	    private readonly Func<Channel> _grpcChannelFactory;

		public GrpcStorageTransportProvider(Func<Channel> grpcChannelFactory)
		{
			_grpcChannelFactory = grpcChannelFactory;
		}

		public static GrpcStorageTransportProvider Create(Func<Channel> grpcChannelFactory = null)
        {
	        return new GrpcStorageTransportProvider(grpcChannelFactory);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new GrpcSpaceTransport(
	            _grpcChannelFactory,
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new GrpcStorageTransport(
	            _grpcChannelFactory,
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}