namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [RequiresAuthenticationToken]
    public class AccountController : ControllerBase<Account, IAccountRepository>
    {
        public AccountController(ILogger logger, IAccountRepository items)
            : base(logger, items)
        {
        }

        public new HttpResponseMessage Get([FromUri]Guid accountId)
        {
            return base.Get(accountId);
        }

        // Get Item by name
        public HttpResponseMessage GetByName([FromUri]string accountName)
        {
            HttpResponseMessage response;
            try
            {
                var account = Items.Get(accountName);
                response = Request.CreateResponse<Account>(HttpStatusCode.OK, account);
            }
            catch (Exception ex)
            {
                Logger.Critical("Unable to serve a Account GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public new HttpResponseMessage Put([FromUri]Guid accountId, Account account)
        {
            return base.Put(accountId, account);
        }

        public new HttpResponseMessage Delete([FromUri]Guid accountId)
        {
            return base.Delete(accountId);
        }
    }    
}
