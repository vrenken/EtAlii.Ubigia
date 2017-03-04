namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRSpaceTransport : SpaceTransportBase, ISignalRSpaceTransport
    {
        public HubConnection HubConnection => _hubConnection;
        private HubConnection _hubConnection;

        public IHttpClient HttpClient => _httpClient;
        private readonly IHttpClient _httpClient;

        public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRSpaceTransport(
            IHttpClient httpClient, 
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            _httpClient = httpClient;
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

        public override void Initialize(ISpaceConnection spaceConnection, string address)
        {
            if (_hubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
            }
            _hubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData);
        }

        public override async Task Start(ISpaceConnection spaceConnection, string address)
        {
            await base.Start(spaceConnection, address);

            // TODO: Dang, we do not use websockets but server-side events.... 
            // Could this be improved by somehow creating a autotransport with WebSocketTransport inside? 
            // This requires the .Start call to be made in a non-PCL project which allows instantiation of the WebSocketTransport class.
            await _hubConnection.Start(_httpClient);
        }

        public override async Task Stop(ISpaceConnection spaceConnection)
        {
            if (_hubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(spaceConnection);

            _hubConnection.Dispose();
            _hubConnection = null;
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
