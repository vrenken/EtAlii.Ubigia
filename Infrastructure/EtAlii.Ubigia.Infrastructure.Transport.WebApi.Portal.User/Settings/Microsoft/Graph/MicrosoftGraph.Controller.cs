// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.Ubigia.Provisioning.Microsoft.Graph;

    [RoutePrefix(RelativeUri.User.MicrosoftGraphSettings), CacheWebApi(-1)]
    public class MicrosoftGraphUserController : UserControllerBase
    {
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;
        private readonly IUserSettingsGetter _userSettingsGetter;
        private readonly IUserSettingsSetter _userSettingsSetter;
        private readonly IUserSettingsClearer _userSettingsClearer;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public MicrosoftGraphUserController(
            ISystemConnectionCreationProxy connectionCreationProxy,
            IUserSettingsGetter userSettingsGetter,
            IUserSettingsSetter userSettingsSetter,
            IUserSettingsClearer userSettingsClearer,
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _connectionCreationProxy = connectionCreationProxy;
            _userSettingsGetter = userSettingsGetter;
            _userSettingsSetter = userSettingsSetter;
            _userSettingsClearer = userSettingsClearer;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        [Route, HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(ActionContext);
            var accountName = authenticationToken.Name;

            return CreateHttpResponse(request, () =>
            {
                var userConnection = _connectionCreationProxy.Request();
                IDataConnection spaceConnection = null;
                var task = Task.Run(async () =>
                {
                    spaceConnection = await userConnection.OpenSpace(accountName, SpaceName.Configuration);
                });
                task.Wait();
                var context = new DataContextFactory().Create(spaceConnection);
                var settings = _userSettingsGetter.Get(context)
                .Select(s => new
                {
                    Name = accountName,
                    PrivateKey = s.PrivateKey,
                    Created = s.Created,
                    Updated = s.Updated,
                }).ToArray();

                return request.CreateResponse(HttpStatusCode.OK, settings);
            });
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, dynamic settings)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(ActionContext);
            var accountName = authenticationToken.Name;

            return CreateHttpResponse(request, () =>
            {
                string userName = settings.UserName;
                var userSettings = new UserSettings
                {
                    PrivateKey = settings.PrivateKey,
                    Created = settings.Created,
                    Updated = settings.Updated
                };

                var userConnection = _connectionCreationProxy.Request();
                IDataConnection spaceConnection = null;
                var task = Task.Run(async () =>
                {
                    spaceConnection = await userConnection.OpenSpace(accountName, SpaceName.Configuration);
                });
                task.Wait();
                var context = new DataContextFactory().Create(spaceConnection);
                _userSettingsSetter.Set(context, userName, settings);

                return request.CreateResponse(HttpStatusCode.OK, userSettings);
            });
        }

        [Route, HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, dynamic settings)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(ActionContext);
            var accountName = authenticationToken.Name;

            return CreateHttpResponse(request, () =>
            {
                string userName = settings.UserName;
                var userConnection = _connectionCreationProxy.Request();
                IDataConnection spaceConnection = null;
                var task = Task.Run(async () =>
                {
                    spaceConnection = await userConnection.OpenSpace(accountName, SpaceName.Configuration);
                });
                task.Wait();
                var context = new DataContextFactory().Create(spaceConnection);
                _userSettingsClearer.Clear(context, userName);

                return request.CreateResponse(HttpStatusCode.OK);
            });
        }
    }
}
