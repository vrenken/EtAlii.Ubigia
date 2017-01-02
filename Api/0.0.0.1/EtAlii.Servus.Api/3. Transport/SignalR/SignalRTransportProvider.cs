namespace EtAlii.Servus.Api.Transport.SignalR
{
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRTransportProvider : ITransportProvider
    {
        public IHttpClient HttpClient { get { return _httpClient; } }
        private readonly IHttpClient _httpClient;

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
            return new SignalRSpaceTransport(_httpClient);
        }
    }
}