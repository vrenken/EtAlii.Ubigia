namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

	public class GrpcSpaceTransport : SpaceTransportBase, IGrpcSpaceTransport
    {
	    public Channel Channel => GetChannel();
	    private Channel _channel;

	    public Metadata AuthenticationHeaders { get; set; }
	    
	    public string AuthenticationToken { get => _authenticationTokenProvider.AuthenticationToken; set => _authenticationTokenProvider.AuthenticationToken = value; }
	    private readonly IAuthenticationTokenProvider _authenticationTokenProvider;
	    private readonly Func<Uri, Channel> _grpcChannelFactory;
	     
        public GrpcSpaceTransport(
	        Uri address,
	        Func<Uri, Channel> grpcChannelFactory, 
	        IAuthenticationTokenProvider authenticationTokenProvider)
	        : base(address)
        {
	        _grpcChannelFactory = grpcChannelFactory;
	        _authenticationTokenProvider = authenticationTokenProvider;
        }

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
                new GrpcSpaceClientsScaffolding()
            };
        }
    }
}
