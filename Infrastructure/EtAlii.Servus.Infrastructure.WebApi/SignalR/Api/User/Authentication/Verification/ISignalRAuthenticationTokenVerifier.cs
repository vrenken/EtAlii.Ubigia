namespace EtAlii.Servus.Infrastructure.SignalR
{
    public interface ISignalRAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}