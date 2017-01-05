namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    public interface ISignalRAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}