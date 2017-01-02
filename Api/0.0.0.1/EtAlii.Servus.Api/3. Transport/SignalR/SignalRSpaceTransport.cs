namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRSpaceTransport : SpaceTransportBase, ISignalRSpaceTransport
    {
        public HubConnection HubConnection { get { return _hubConnection; } }
        private HubConnection _hubConnection;

        public IHttpClient HttpClient {get { return _httpClient; } }
        private readonly IHttpClient _httpClient;

        public SignalRSpaceTransport(IHttpClient httpClient)
        {
            _httpClient = httpClient;
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

        //public override async Task Open(ISpaceConnection spaceConnection, string address)
        //{
        //    if (_hubConnection != null)
        //    {
        //        throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
        //    }
        //    _hubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData);

        //    await base.Open(spaceConnection, address);

        //    await _hubConnection.Start(_httpClient);
        //}

        //public override async Task Close(ISpaceConnection spaceConnection)
        //{
        //    if (_hubConnection == null)
        //    {
        //        throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
        //    }

        //    await base.Close(spaceConnection);

        //    _hubConnection.Dispose();
        //    _hubConnection = null;
        //}

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new SignalRSpaceClientsScaffolding(),
            };
        }

        //protected override void InvokeInternal(Action<ISpaceTransport> invocation)
        //{
        //    invocation(this);
        //}
    }
}
