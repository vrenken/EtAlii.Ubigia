namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    public interface ISimpleAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}