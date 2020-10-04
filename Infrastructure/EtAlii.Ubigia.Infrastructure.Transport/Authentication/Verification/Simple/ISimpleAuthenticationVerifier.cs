namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface ISimpleAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier, params string[] requiredRoles);
        string Verify(string accountName, string password, string hostIdentifier, out Account account, params string[] requiredRoles);
    }
}