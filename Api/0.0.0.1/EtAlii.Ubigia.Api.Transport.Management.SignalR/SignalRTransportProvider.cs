namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Microsoft.AspNet.SignalR.Client.Http;

    public class SignalRStorageTransportProvider : IStorageTransportProvider
    {
        public IHttpClient HttpClient => _httpClient;
        private readonly IHttpClient _httpClient;
        
        private string _authenticationToken;

        private SignalRStorageTransportProvider(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static SignalRStorageTransportProvider Create()
        {
            return new SignalRStorageTransportProvider(new DefaultHttpClient());
        }

        public static SignalRStorageTransportProvider Create(IHttpClient httpClient)
        {
            return new SignalRStorageTransportProvider(httpClient);
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
                _httpClient, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new SignalRStorageTransport(
                _httpClient, 
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}