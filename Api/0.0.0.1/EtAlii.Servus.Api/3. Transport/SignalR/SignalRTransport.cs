namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRTransport : TransportBase, ISignalRTransport
    {
        public HubConnection HubConnection { get { return _hubConnection; } }
        private HubConnection _hubConnection;

        private readonly IHttpClient _signalRHttpClient;

        public static SignalRTransport CreateDefault()
        {
            return new SignalRTransport(new DefaultHttpClient());
        }
 
        public SignalRTransport(
            IHttpClient signalRHttpClient)
        {
            _signalRHttpClient = signalRHttpClient;
        }

        public override async Task Open(string address)
        {
            if (_hubConnection != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.AlreadySubscribedToTransport);
            }
            _hubConnection = new HubConnectionFactory().Create(address + RelativeUri.UserData);

            await base.Open(address);

            await _hubConnection.Start(_signalRHttpClient);
        }

        public override async Task Close()
        {
            if (_hubConnection == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NotSubscribedToTransport);
            }

            await base.Close();

            _hubConnection.Dispose();
            _hubConnection = null;
        }

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new SignalRTransportClientsScaffolding(),
            };
        }

        protected override void InvokeInternal(Action<ITransport> invocation)
        {
            invocation(this);
        }
    }
}
