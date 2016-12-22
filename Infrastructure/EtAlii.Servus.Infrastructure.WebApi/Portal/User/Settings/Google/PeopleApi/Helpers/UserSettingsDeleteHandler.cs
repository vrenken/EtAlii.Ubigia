// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Transport;
    using EtAlii.Servus.Provisioning.Google.PeopleApi;

    public class UserSettingsDeleteHandler : IUserSettingsDeleteHandler
    {
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;
        private readonly IUserSettingsClearer _userSettingsClearer;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public UserSettingsDeleteHandler(
            IUserSettingsClearer userSettingsClearer, 
            ISystemConnectionCreationProxy connectionCreationProxy, 
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _userSettingsClearer = userSettingsClearer;
            _connectionCreationProxy = connectionCreationProxy;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public HttpResponseMessage Delete(dynamic settings, HttpRequestMessage request, HttpActionContext actionContext)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(actionContext);
            var accountName = authenticationToken.Name;

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
        }
    }
}