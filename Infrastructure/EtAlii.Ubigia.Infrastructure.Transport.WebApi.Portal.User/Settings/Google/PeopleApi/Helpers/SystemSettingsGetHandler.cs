// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;

    public class SystemSettingsGetHandler : ISystemSettingsGetHandler
    {
        private readonly ISystemSettingsGetter _systemSettingsGetter;
        private readonly ISystemConnectionCreationProxy _connectionCreationProxy;

        public SystemSettingsGetHandler(
            ISystemSettingsGetter systemSettingsGetter, 
            ISystemConnectionCreationProxy connectionCreationProxy)
        {
            _systemSettingsGetter = systemSettingsGetter;
            _connectionCreationProxy = connectionCreationProxy;
        }

        public HttpResponseMessage Get(HttpRequestMessage request, HttpActionContext actionContext)
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
        }
    }
}