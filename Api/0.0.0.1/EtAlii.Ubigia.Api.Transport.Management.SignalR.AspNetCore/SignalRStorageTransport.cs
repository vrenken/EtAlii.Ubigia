namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRStorageTransport : StorageTransportBase, ISignalRStorageTransport
    {
		private bool _started;

		public string AuthenticationToken { get => _authenticationTokenGetter(); set => _authenticationTokenSetter(value); }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;
	    private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;

        public SignalRStorageTransport(
	        Func<HttpMessageHandler> httpMessageHandlerFactory,
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
	        _httpMessageHandlerFactory = httpMessageHandlerFactory;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

	    public HttpMessageHandler HttpMessageHandlerFactory( )
	    {
		    return _httpMessageHandlerFactory();
	    }

		public override void Initialize(IStorageConnection storageConnection, Uri address)
		{
			if (_started)
			{
				throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
			}
		}

		public override async Task Start(IStorageConnection storageConnection, Uri address)
        {
            await base.Start(storageConnection, address);

	        _started = true;
        }


        public override async Task Stop(IStorageConnection storageConnection)
        {
            if (!_started)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(storageConnection);
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new SignalRStorageClientsScaffolding()
            };
        }
    }
}
