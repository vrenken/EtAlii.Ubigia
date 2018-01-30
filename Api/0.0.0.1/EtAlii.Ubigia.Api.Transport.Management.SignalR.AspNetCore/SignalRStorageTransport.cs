namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRStorageTransport : StorageTransportBase, ISignalRStorageTransport
    {
		private bool _started;

		public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRStorageTransport(
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

		public override void Initialize(IStorageConnection storageConnection, string address)
		{
			if (_started)
			{
				throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
			}
		}

		public override async Task Start(IStorageConnection storageConnection, string address)
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
