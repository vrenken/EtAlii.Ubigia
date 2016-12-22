namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleIdentityProvider
    {
        GoogleIdentity GetGoogleIdentity(GoogleAuthenticationToken googleAuthenticationToken);
    }
}