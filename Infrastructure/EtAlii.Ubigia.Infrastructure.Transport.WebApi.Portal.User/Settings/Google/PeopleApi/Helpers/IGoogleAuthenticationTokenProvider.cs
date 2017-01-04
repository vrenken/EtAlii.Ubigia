namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleAuthenticationTokenProvider
    {
        GoogleAuthenticationToken GetRefreshToken(string authorizationCode);
    }
}