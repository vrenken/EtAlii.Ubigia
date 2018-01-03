namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNetCore.SignalR.Client;

    public class SignalRStorageTransport : StorageTransportBase, ISignalRStorageTransport
    {
        public HubConnection HubConnection { get; private set; }

        public ClientHttpMessageHandler HttpClientHandler { get; }

        public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRStorageTransport(
            ClientHttpMessageHandler httpClientHandler, 
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            HttpClientHandler = httpClientHandler;
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

        public override void Initialize(IStorageConnection storageConnection, string address)
        {
            if (HubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
            }
            HubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData, HttpClientHandler);
        }

        public override async Task Start(IStorageConnection storageConnection, string address)
        {
            await base.Start(storageConnection, address);

            await HubConnection.StartAsync();
        }


        public override async Task Stop(IStorageConnection storageConnection)
        {
            if (HubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(storageConnection);

            await HubConnection.DisposeAsync();
            HubConnection = null;
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
