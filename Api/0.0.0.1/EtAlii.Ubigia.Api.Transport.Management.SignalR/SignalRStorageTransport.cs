namespace EtAlii.Ubigia.Api.Management.SignalR
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
        public HubConnection HubConnection { get { return _hubConnection; } }
        private HubConnection _hubConnection;

        public IHttpClient HttpClient { get { return _httpClient; } }
        private readonly IHttpClient _httpClient;

        public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRStorageTransport(
            IHttpClient httpClient, 
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            _httpClient = httpClient;
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

        public override void Initialize(IStorageConnection storageConnection, string address)
        {
            if (_hubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
            }
            _hubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData);
        }

        public override async Task Start(IStorageConnection storageConnection, string address)
        {
            await base.Start(storageConnection, address);

            await _hubConnection.Start(_httpClient);
        }


        public override async Task Stop(IStorageConnection storageConnection)
        {
            if (_hubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(storageConnection);

            _hubConnection.Dispose();
            _hubConnection = null;
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
