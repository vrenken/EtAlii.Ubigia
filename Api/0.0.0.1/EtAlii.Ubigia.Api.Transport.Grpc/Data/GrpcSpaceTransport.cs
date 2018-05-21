namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

	public class GrpcSpaceTransport : SpaceTransportBase, IGrpcSpaceTransport
    {
	    private bool _started;

	    public Channel Channel { get; }

	    public Metadata AuthenticationHeaders { get; set; }
	    
		public string AuthenticationToken { get => _authenticationTokenGetter(); set => _authenticationTokenSetter(value); }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public GrpcSpaceTransport(
	        Channel channel, 
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
	        Channel = channel;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
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

	        // TODO: Dang, we do not use websockets but server-side events.... 
	        // Could this be improved by somehow creating a autotransport with WebSocketTransport inside? 
	        // This requires the .Start call to be made in a non-PCL project which allows instantiation of the WebSocketTransport class.
        }

        public override async Task Stop(ISpaceConnection spaceConnection)
        {
            if (!_started)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(spaceConnection);
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
