// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using global::Grpc.Core;
	using global::Grpc.Net.Client;
    using EtAlii.xTechnology.Threading;

	public class GrpcStorageTransportProvider : IStorageTransportProvider
    {
        private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;
        private readonly IContextCorrelator _contextCorrelator;

        private readonly AuthenticationTokenProvider _storageAuthenticationTokenProvider;

		private GrpcStorageTransportProvider(Func<Uri, GrpcChannel> grpcChannelFactory, IContextCorrelator contextCorrelator)
		{
			_grpcChannelFactory = grpcChannelFactory;
            _contextCorrelator = contextCorrelator;
            _storageAuthenticationTokenProvider = new AuthenticationTokenProvider();
		}

        public ISpaceTransport GetSpaceTransport(Uri address)
        {
	        // We always use a new authenticationTokenProvider for space based access.
	        var authenticationTokenProvider = new AuthenticationTokenProvider { AuthenticationToken = _storageAuthenticationTokenProvider.AuthenticationToken };
            return new GrpcSpaceTransport(address, _grpcChannelFactory, authenticationTokenProvider, _contextCorrelator);
        }

        public IStorageTransport GetStorageTransport(Uri address)
        {
	        // We always want to use the same authenticationTokenProvider for storage based access.
	        var authenticationTokenProvider = _storageAuthenticationTokenProvider;
            return new GrpcStorageTransport(address, _grpcChannelFactory, authenticationTokenProvider, _contextCorrelator);
        }


	    public static GrpcStorageTransportProvider Create(Func<Uri, GrpcChannel> channelFactory, IContextCorrelator contextCorrelator)
	    {
		    return new(channelFactory, contextCorrelator);
	    }

	    public static GrpcStorageTransportProvider Create(IContextCorrelator contextCorrelator)
	    {
		    var channelFactory = new Func<Uri, GrpcChannel>(channelAddress =>
		    {
			    var options = new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure };
			    return GrpcChannel.ForAddress(channelAddress, options);
		    });
		    return new GrpcStorageTransportProvider(channelFactory, contextCorrelator);
	    }
    }
}
