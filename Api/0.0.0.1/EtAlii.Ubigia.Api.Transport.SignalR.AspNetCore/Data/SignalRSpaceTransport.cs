namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRSpaceTransport : SpaceTransportBase, ISignalRSpaceTransport
    {
	    private bool _started;

        public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRSpaceTransport(
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

		public override void Initialize(ISpaceConnection spaceConnection, string address)
		{
			if (_started)
			{
				throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
			}
		}

		public override async Task Start(ISpaceConnection spaceConnection, string address)
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
                new SignalRSpaceClientsScaffolding()
            };
        }
    }
}
