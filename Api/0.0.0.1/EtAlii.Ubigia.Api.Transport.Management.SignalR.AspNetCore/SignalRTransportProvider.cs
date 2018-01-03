namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public class SignalRStorageTransportProvider : IStorageTransportProvider
    {
        public ClientHttpMessageHandler HttpClientHandler { get; }

        private string _authenticationToken;

        private SignalRStorageTransportProvider(ClientHttpMessageHandler httpClientHandler)
        {
            HttpClientHandler = httpClientHandler;
        }

        public static SignalRStorageTransportProvider Create()
        {
            return new SignalRStorageTransportProvider(new ClientHttpMessageHandler());
        }

        public static SignalRStorageTransportProvider Create(ClientHttpMessageHandler httpClientHandler)
        {
            return new SignalRStorageTransportProvider(httpClientHandler);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
                HttpClientHandler, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new SignalRStorageTransport(
                HttpClientHandler, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}