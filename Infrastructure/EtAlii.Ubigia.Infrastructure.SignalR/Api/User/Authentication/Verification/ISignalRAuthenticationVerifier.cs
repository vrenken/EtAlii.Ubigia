namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    public interface ISignalRAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier);
    }
}