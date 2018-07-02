namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

	public class GrpcStorageTransport : StorageTransportBase, IGrpcStorageTransport
    {
	    public Channel Channel => GetChannel();
	    private Channel _channel;

	    public Metadata AuthenticationHeaders { get; set; }
	    
		public string AuthenticationToken { get => _authenticationTokenGetter(); set => _authenticationTokenSetter(value); }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;
	    private readonly Func<Uri, Channel> _grpcChannelFactory;

        public GrpcStorageTransport(Uri address,
	        Func<Uri, Channel> grpcChannelFactory, 
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
	        : base(address)
        {
	        _grpcChannelFactory = grpcChannelFactory;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
		}

	    /// <summary>
	    /// Gets a channel based on the specified Uri. 
	    /// </summary>
	    /// <returns></returns>
	    private Channel GetChannel()
	    {
		    var uriAsString= _channel?.ResolvedTarget;
		    var hasAddress = !String.IsNullOrWhiteSpace(uriAsString);
		    if (hasAddress)
		    {
			    var channelAddress = new Uri("http://" + uriAsString);

			    var hasSameHost = String.Equals(Address.DnsSafeHost, channelAddress.DnsSafeHost, StringComparison.InvariantCultureIgnoreCase);
			    var hasSamePort = Address.Port == channelAddress.Port;
			    if (hasSameHost && hasSamePort)
			    {
				    return _channel;
			    }
		    }
		     
		    return _channel = _grpcChannelFactory.Invoke(Address);
	    }

        public override async Task Stop()
        {
            await base.Stop();

	        if (_channel.State != ChannelState.Shutdown)
	        {
		        await _channel.ShutdownAsync();
	        }
	        _channel = null;
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
