namespace EtAlii.Ubigia.Infrastructure
{
    using System.Web.Http.Controllers;

    public interface IAuthenticationTokenConverter
    {
        byte[] ToBytes(AuthenticationToken token);
        AuthenticationToken FromBytes(byte[] tokenAsBytes);
        AuthenticationToken FromString(string authenticationTokenAsString);
        AuthenticationToken FromHttpActionContext(HttpActionContext actionContext);
    }
}