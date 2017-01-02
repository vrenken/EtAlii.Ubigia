namespace EtAlii.Servus.Infrastructure.SignalR
{
    public interface ISignalRAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier);
    }
}