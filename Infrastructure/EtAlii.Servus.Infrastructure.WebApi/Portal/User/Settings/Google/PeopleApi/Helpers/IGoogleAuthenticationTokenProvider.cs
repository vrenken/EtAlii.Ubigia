namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleAuthenticationTokenProvider
    {
        GoogleAuthenticationToken GetRefreshToken(string authorizationCode);
    }
}