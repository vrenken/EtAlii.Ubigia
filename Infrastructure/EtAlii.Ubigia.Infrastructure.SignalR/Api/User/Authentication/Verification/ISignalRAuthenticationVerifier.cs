namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    public interface ISignalRAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier);
    }
}