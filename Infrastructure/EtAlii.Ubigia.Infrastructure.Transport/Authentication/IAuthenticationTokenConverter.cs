namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface IAuthenticationTokenConverter
    {
        byte[] ToBytes(AuthenticationToken token);
        AuthenticationToken FromBytes(byte[] tokenAsBytes);
        AuthenticationToken FromString(string authenticationTokenAsString);
    }
}