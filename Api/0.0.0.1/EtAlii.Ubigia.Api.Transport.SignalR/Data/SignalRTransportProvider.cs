namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRTransportProvider : ITransportProvider
    {
        public IHttpClient HttpClient { get; }

        private string _authenticationToken;

        private SignalRTransportProvider(IHttpClient httpClient) 
        {
            HttpClient = httpClient;
        }

        public static SignalRTransportProvider Create()
        {
            return new SignalRTransportProvider(new DefaultHttpClient());
        }

        public static SignalRTransportProvider Create(IHttpClient httpClient)
        {
            return new SignalRTransportProvider(httpClient);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
                HttpClient, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}