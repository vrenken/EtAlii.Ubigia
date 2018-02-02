namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User
{
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;

    [RoutePrefix(RelativeUri.User.GooglePeopleApiSetting), CacheWebApi(-1)]
    public class GooglePeopleApiUserController : UserControllerBase
    {
        private readonly IUserSettingsPostHandler _userSettingsPostHandler;
        private readonly IUserSettingsGetHandler _userSettingsGetHandler;
        private readonly ISystemSettingsGetHandler _systemSettingsGetHandler;
        private readonly IUserSettingsDeleteHandler _userSettingsDeleteHandler;

        public GooglePeopleApiUserController(
            IUserSettingsPostHandler userSettingsPostHandler, 
            IUserSettingsGetHandler userSettingsGetHandler, 
            ISystemSettingsGetHandler systemSettingsGetHandler, 
            IUserSettingsDeleteHandler userSettingsDeleteHandler)
        {
            _userSettingsPostHandler = userSettingsPostHandler;
            _userSettingsGetHandler = userSettingsGetHandler;
            _systemSettingsGetHandler = systemSettingsGetHandler;
            _userSettingsDeleteHandler = userSettingsDeleteHandler;
        }

        [Route("SystemSettings"), HttpGet]
        public HttpResponseMessage GetSystemSettings(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => _systemSettingsGetHandler.Get(request, ActionContext));
        }


        [Route, HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => _userSettingsGetHandler.GetAll(request, ActionContext));
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, dynamic settings)
        {
            return CreateHttpResponse(request, () => _userSettingsPostHandler.Post(settings, request, ActionContext));
        }

        [Route, HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, dynamic settings)
        {
            return CreateHttpResponse(request, () => _userSettingsDeleteHandler.Delete(settings, request, ActionContext));
        }
    }
}
