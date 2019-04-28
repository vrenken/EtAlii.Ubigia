namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;

    public interface ISignalRAuthenticationTokenGetter
    {
        Task<string> GetAuthenticationToken(ISignalRSpaceTransport transport, string accountName, string password, string authenticationToken);
        Task<string> GetAuthenticationToken(ISignalRStorageTransport transport, string accountName, string password, string authenticationToken);
    }
}