// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System.Net.Http;
    using System.Web.Http.Controllers;

    public interface IUserSettingsGetHandler
    {
        dynamic Get(string accountName);
        HttpResponseMessage GetAll(HttpRequestMessage request, HttpActionContext actionContext);
    }
}