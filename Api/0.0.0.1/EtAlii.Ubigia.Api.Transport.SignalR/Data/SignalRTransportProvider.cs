namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRTransportProvider : ITransportProvider
    {
        public IHttpClient HttpClient => _httpClient;
        private readonly IHttpClient _httpClient;

        private string _authenticationToken;

        private SignalRTransportProvider(IHttpClient httpClient) 
        {
            _httpClient = httpClient;
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
                _httpClient, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}