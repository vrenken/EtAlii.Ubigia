namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface ISimpleAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}