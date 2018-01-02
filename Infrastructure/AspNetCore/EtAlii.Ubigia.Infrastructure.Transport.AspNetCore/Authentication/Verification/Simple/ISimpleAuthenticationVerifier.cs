namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    public interface ISimpleAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier);
    }
}