namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RequiresAuthenticationToken(Role.Admin, Role.System)]
    [Route(RelativeUri.Data.Accounts)]
    public class AccountController : ApiController
    {
        private readonly IAccountRepository _items;

        //protected ILogger Logger { get { return _logger; } }
        //private readonly ILogger _logger;

        public AccountController(IAccountRepository items)
        {
            _items = items;
        }

        public HttpResponseMessage GetByName([FromUri]string accountName)
        {
            HttpResponseMessage response;
            try
            {
                var account = _items.Get(accountName);
                response = Request.CreateResponse(HttpStatusCode.OK, account);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Account GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }


        // Get all Items
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var items = _items.GetAll();
                response = Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Get Item by id
        public HttpResponseMessage Get([FromUri]Guid accountId)
        {
            HttpResponseMessage response;
            try
            {
                var item = _items.Get(accountId);
                response = Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Add item
        public HttpResponseMessage Post([FromBody]Account item, string accountTemplate)
        {
            HttpResponseMessage response;
            try
            {
                var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);
                item = _items.Add(item, template);
                response = Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Update Item by id
        public HttpResponseMessage Put([FromUri]Guid accountId, Account account)
        {
            HttpResponseMessage response;
            try
            {
                var result = _items.Update(accountId, account);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} PUT client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        // Delete Item by id
        public HttpResponseMessage Delete([FromUri]Guid accountId)
        {
            HttpResponseMessage response;
            try
            {
                _items.Remove(accountId);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} DELETE client request", ex, typeof(T).Name);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }    
}
