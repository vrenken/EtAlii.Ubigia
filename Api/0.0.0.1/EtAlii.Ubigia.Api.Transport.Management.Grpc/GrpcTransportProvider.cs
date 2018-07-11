namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;
	using global::Grpc.Core;

	public class GrpcStorageTransportProvider : IStorageTransportProvider
    {
        private readonly Func<Uri, Channel> _grpcChannelFactory;

		public GrpcStorageTransportProvider(Func<Uri, Channel> grpcChannelFactory)
		{
			_grpcChannelFactory = grpcChannelFactory;
		}

		public static GrpcStorageTransportProvider Create(Func<Uri, Channel> grpcChannelFactory = null)
        {
	        return new GrpcStorageTransportProvider(grpcChannelFactory);
        }

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
            return new GrpcSpaceTransport(
	            address,
	            _grpcChannelFactory,
	            new AuthenticationTokenProvider());
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
            return new GrpcStorageTransport(
	            address,
	            _grpcChannelFactory,
	            new AuthenticationTokenProvider());
        }
    }
}