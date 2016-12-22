﻿namespace EtAlii.Servus.Infrastructure.WebApi.Portal.User
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Servus.Api.Transport;

    [RequiresAuthenticationToken(Role.User)]
    public class UserControllerBase : ApiController
    {
        internal UserControllerBase()
        {
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (Exception ex)
            {
                //LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
    }
}
