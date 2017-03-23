namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRStorageTransport : StorageTransportBase, ISignalRStorageTransport
    {
        public HubConnection HubConnection { get; private set; }

        public IHttpClient HttpClient { get; }

        public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRStorageTransport(
            IHttpClient httpClient, 
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            HttpClient = httpClient;
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

        public override void Initialize(IStorageConnection storageConnection, string address)
        {
            if (HubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
            }
            HubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData);
        }

        public override async Task Start(IStorageConnection storageConnection, string address)
        {
            await base.Start(storageConnection, address);

            await HubConnection.Start(HttpClient);
        }


        public override async Task Stop(IStorageConnection storageConnection)
        {
            if (HubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(storageConnection);

            HubConnection.Dispose();
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
