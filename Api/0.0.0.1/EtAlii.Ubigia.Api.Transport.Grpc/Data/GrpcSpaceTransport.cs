namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

	public class GrpcSpaceTransport : SpaceTransportBase, IGrpcSpaceTransport
    {
	    private bool _started;

	    public Channel Channel => GetChannel();
	    private Channel _channel;

	    public Metadata AuthenticationHeaders { get; set; }
	    
		public string AuthenticationToken { get => _authenticationTokenGetter(); set => _authenticationTokenSetter(value); }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;
	    private readonly Func<Channel> _grpcChannelFactory;
	    
        public GrpcSpaceTransport(
	        Func<Channel> grpcChannelFactory, 
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
	        _grpcChannelFactory = grpcChannelFactory;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

	    private Channel GetChannel()
	    {
		    return _channel ?? (_channel = _grpcChannelFactory.Invoke());
	    }

		public override void Initialize(ISpaceConnection spaceConnection, Uri address)
		{
			if (_started)
			{
				throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
			}
		}

		public override async Task Start(ISpaceConnection spaceConnection, Uri address)
        {
            await base.Start(spaceConnection, address);
	        
	        _started = true;
        }

        public override async Task Stop(ISpaceConnection spaceConnection)
        {
            if (!_started)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(spaceConnection);

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
