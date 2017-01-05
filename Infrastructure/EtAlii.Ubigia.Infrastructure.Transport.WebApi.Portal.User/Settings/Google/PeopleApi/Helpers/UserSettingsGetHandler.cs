// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;

    public class UserSettingsGetHandler : IUserSettingsGetHandler
    {
        private readonly IUserSettingsGetter _userSettingsGetter;
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;
        private readonly IAuthenticationTokenConverter _authenticationTokenConverter;

        public UserSettingsGetHandler(
            IUserSettingsGetter userSettingsGetter, 
            ISystemConnectionCreationProxy connectionCreationProxy, 
            IAuthenticationTokenConverter authenticationTokenConverter)
        {
            _userSettingsGetter = userSettingsGetter;
            _connectionCreationProxy = connectionCreationProxy;
            _authenticationTokenConverter = authenticationTokenConverter;
        }

        public dynamic Get(string accountName)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage GetAll(HttpRequestMessage request, HttpActionContext actionContext)
        {
            var authenticationToken = _authenticationTokenConverter.FromHttpActionContext(actionContext);
            var accountName = authenticationToken.Name;

            var userConnection = _connectionCreationProxy.Request();
            IDataConnection spaceConnection = null;
            var task = Task.Run(async () =>
            {
                spaceConnection = await userConnection.OpenSpace(accountName, SpaceName.Configuration);
            });
            task.Wait();
            var context = new DataContextFactory().Create(spaceConnection);
            var allSettings = _userSettingsGetter.Get(context);
            return request.CreateResponse(HttpStatusCode.OK, allSettings);
        }
    }
}