namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRSpaceTransport : SpaceTransportBase, ISignalRSpaceTransport
    {
        public HubConnection HubConnection { get; private set; }

        public IHttpClient HttpClient { get; }

        public string AuthenticationToken { get { return _authenticationTokenGetter(); } set { _authenticationTokenSetter(value); } }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;

        public SignalRSpaceTransport(
            IHttpClient httpClient, 
            Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        {
            HttpClient = httpClient;
            _authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

        public override void Initialize(ISpaceConnection spaceConnection, Uri address)
        {
            if (HubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
            }
            HubConnection = new HubConnectionFactory().Create(new Uri(address + RelativeUri.UserData));
        }

        public override async Task Start(ISpaceConnection spaceConnection, Uri address)
        {
            await base.Start(spaceConnection, address);

            // TODO: Dang, we do not use websockets but server-side events.... 
            // Could this be improved by somehow creating a autotransport with WebSocketTransport inside? 
            // This requires the .Start call to be made in a non-PCL project which allows instantiation of the WebSocketTransport class.
            await HubConnection.Start(HttpClient);
        }

        public override async Task Stop(ISpaceConnection spaceConnection)
        {
            if (HubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Stop(spaceConnection);

            HubConnection.Dispose();
            HubConnection = null;
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
