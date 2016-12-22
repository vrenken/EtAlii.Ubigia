﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Infrastructure.Transport;
    using EtAlii.Servus.Provisioning.Google.PeopleApi;

    [RoutePrefix(RelativeUri.Admin.GooglePeopleApiSettings), CacheWebApi(-1)]
    public class GooglePeopleApiAdminController : AdminControllerBase
    {
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;
        private readonly ISystemSettingsGetter _systemSettingsGetter;
        private readonly ISystemSettingsSetter _systemSettingsSetter;

        public GooglePeopleApiAdminController(
            ISystemConnectionCreationProxy connectionCreationProxy, 
            ISystemSettingsGetter systemSettingsGetter, 
            ISystemSettingsSetter systemSettingsSetter)
        {
            _connectionCreationProxy = connectionCreationProxy;
            _systemSettingsGetter = systemSettingsGetter;
            _systemSettingsSetter = systemSettingsSetter;
        }

        [Route, HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var systemConnection = _connectionCreationProxy.Request();
                IDataConnection spaceConnection = null;
                var task = Task.Run(async () =>
                {
                    spaceConnection = await systemConnection.OpenSpace(AccountName.System, SpaceName.System);
                });
                task.Wait();
                var context = new DataContextFactory().Create(spaceConnection);
                var settings = _systemSettingsGetter.Get(context);

                return request.CreateResponse(HttpStatusCode.OK, settings);
            });
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, SystemSettings settings)
        {
            return CreateHttpResponse(request, () =>
            {
                var systemConnection = _connectionCreationProxy.Request();
                IDataConnection spaceConnection = null;
                var task = Task.Run(async () =>
                {
                    spaceConnection = await systemConnection.OpenSpace(AccountName.System, SpaceName.System);
                });
                task.Wait();
                var context = new DataContextFactory().Create(spaceConnection);
                _systemSettingsSetter.Set(context, settings);

                return request.CreateResponse(HttpStatusCode.OK, settings);
            });
        }
    }
}
