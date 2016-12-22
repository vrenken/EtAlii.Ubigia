// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using System.Net.Http;
    using System.Web.Http.Controllers;

    public interface IUserSettingsDeleteHandler
    {
        HttpResponseMessage Delete(dynamic settings, HttpRequestMessage request, HttpActionContext actionContext);
    }
}