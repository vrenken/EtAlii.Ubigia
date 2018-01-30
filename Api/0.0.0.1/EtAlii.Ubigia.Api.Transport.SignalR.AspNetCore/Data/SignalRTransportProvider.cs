namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    public class SignalRTransportProvider : ITransportProvider
    {
        private string _authenticationToken;

        public static SignalRTransportProvider Create()
        {
	        return new SignalRTransportProvider();//new ClientHttpMessageHandler());
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}