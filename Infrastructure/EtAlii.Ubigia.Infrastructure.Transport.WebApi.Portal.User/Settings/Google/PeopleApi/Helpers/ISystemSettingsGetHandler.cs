namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    using System.Net.Http;
    using System.Web.Http.Controllers;

    public interface ISystemSettingsGetHandler
    {
        HttpResponseMessage Get(HttpRequestMessage request, HttpActionContext actionContext);
    }
}