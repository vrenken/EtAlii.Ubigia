// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
	using System;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using EtAlii.xTechnology.MicroContainer;
	using global::Grpc.Core;
	using global::Grpc.Net.Client;
    using EtAlii.xTechnology.Threading;
    using global::Grpc.Core.Interceptors;


	public class GrpcStorageTransport : StorageTransportBase, IGrpcStorageTransport
    {
	    public CallInvoker CallInvoker => GetCallInvoker();
        private CallInvoker _callInvoker;
        private GrpcChannel _channel;

	    public Metadata.Entry AuthenticationHeader { get; set; }

		public string AuthenticationToken { get => _authenticationTokenProvider.AuthenticationToken; set => _authenticationTokenProvider.AuthenticationToken = value; }
	    private readonly IAuthenticationTokenProvider _authenticationTokenProvider;
	    private readonly Func<Uri, GrpcChannel> _grpcChannelFactory;
        private readonly IContextCorrelator _contextCorrelator;

        public GrpcStorageTransport(Uri address,
	        Func<Uri, GrpcChannel> grpcChannelFactory,
	        IAuthenticationTokenProvider authenticationTokenProvider,
            IContextCorrelator contextCorrelator)
	        : base(address)
        {
	        _grpcChannelFactory = grpcChannelFactory;
	        _authenticationTokenProvider = authenticationTokenProvider;
            _contextCorrelator = contextCorrelator;
        }

	    /// <summary>
	    /// Gets a CallInvoker based on the specified Uri.
	    /// </summary>
	    /// <returns></returns>
	    private CallInvoker GetCallInvoker()
	    {
		    var uriAsString = _channel?.Target;
		    var hasAddress = !string.IsNullOrWhiteSpace(uriAsString);
		    if (hasAddress)
		    {
			    var channelAddress = new Uri($"http://{uriAsString}");

			    var hasSameHost = string.Equals(Address.DnsSafeHost, channelAddress.DnsSafeHost, StringComparison.InvariantCultureIgnoreCase);
			    var hasSamePort = Address.Port == channelAddress.Port;
			    if (hasSameHost && hasSamePort)
			    {
				    return _callInvoker;
			    }
		    }
		    _channel = _grpcChannelFactory.Invoke(Address);
            _callInvoker = _channel.Intercept(new CorrelationCallInterceptor(_contextCorrelator));
            return _callInvoker;
        }

        public override async Task Stop()
        {
            await base.Stop().ConfigureAwait(false);

	        _channel?.Dispose();
	        _channel = null;
            _callInvoker = null;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new GrpcStorageClientsScaffolding()
            };
        }
    }
}
