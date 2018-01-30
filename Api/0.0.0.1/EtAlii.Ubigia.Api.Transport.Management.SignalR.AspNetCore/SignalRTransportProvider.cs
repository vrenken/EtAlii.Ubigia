namespace EtAlii.Ubigia.Api.Transport.Management.SignalR
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;

    public class SignalRStorageTransportProvider : IStorageTransportProvider
    {
        private string _authenticationToken;

        public static SignalRStorageTransportProvider Create()
        {
	        return new SignalRStorageTransportProvider();
        }

        public ISpaceTransport GetSpaceTransport()
        {
            return new SignalRSpaceTransport(
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }

        public IStorageTransport GetStorageTransport()
        {
            return new SignalRStorageTransport(
                v => _authenticationToken = v, 
                () => _authenticationToken);
        }
    }
}