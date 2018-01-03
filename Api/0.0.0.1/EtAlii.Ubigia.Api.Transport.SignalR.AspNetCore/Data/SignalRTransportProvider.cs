namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Net.Http;

    public class SignalRTransportProvider : ITransportProvider
    {
        public ClientHttpMessageHandler HttpClientHandler { get; }

        private string _authenticationToken;

        private SignalRTransportProvider(ClientHttpMessageHandler httpClientHandler) 
        {
            HttpClientHandler = httpClientHandler;
        }

        public static SignalRTransportProvider Create()
        {
            return new SignalRTransportProvider(new ClientHttpMessageHandler());
        }

        public static SignalRTransportProvider Create(ClientHttpMessageHandler httpClientHandler)
        {
            return new SignalRTransportProvider(httpClientHandler);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
                HttpClientHandler, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}