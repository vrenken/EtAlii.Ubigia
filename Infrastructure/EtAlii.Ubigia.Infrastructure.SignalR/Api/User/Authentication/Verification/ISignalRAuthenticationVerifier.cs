namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    public interface ISignalRAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier);
    }
}