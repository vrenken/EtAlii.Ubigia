namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.xTechnology.MicroContainer;
    using global::Grpc.Core;

	public class GrpcStorageTransport : StorageTransportBase, IGrpcStorageTransport
    {
		private bool _started;

	    public Channel Channel { get; }
	    
	    public Metadata AuthenticationHeaders { get; set; }
	    
		public string AuthenticationToken { get => _authenticationTokenGetter(); set => _authenticationTokenSetter(value); }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public GrpcStorageTransport(
	        Channel channel, 
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
	        Channel = channel;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
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
                new GrpcStorageClientsScaffolding()
            };
        }
    }
}
