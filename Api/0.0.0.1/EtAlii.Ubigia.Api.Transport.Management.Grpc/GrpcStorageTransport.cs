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
	    private readonly Func<Channel> _grpcChannelFactory;

        public GrpcStorageTransport(Uri address,
	        Func<Channel> grpcChannelFactory, 
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
	        : base(address)
        {
	        _grpcChannelFactory = grpcChannelFactory;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
		}

	    private Channel GetChannel()
	    {
		    return _channel ?? (_channel = _grpcChannelFactory.Invoke());
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
