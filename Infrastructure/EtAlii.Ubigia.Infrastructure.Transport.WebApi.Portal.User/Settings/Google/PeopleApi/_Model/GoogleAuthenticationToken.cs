namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GoogleAuthenticationToken
    {
        public string access_token;
        public string expires_in;
        public string token_type;
        public string refresh_token;
    }
}