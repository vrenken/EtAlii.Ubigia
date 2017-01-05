namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RoutePrefix(RelativeUri.Admin.AccountAdministration), CacheWebApi(-1)]
    public class AccountAdminController : AdminControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountAdminController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("{accountId:guid}"), HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid accountId)
        {
            return CreateHttpResponse(request, () =>
            {
                var account = _accountRepository.Get(accountId);

                var result = new
                {
                    Id = account.Id,
                    Name = account.Name,
                    Password = account.Password,
                    ConsumedStorage = "2938 MB",
                    NumberOfSpaces = "2",
                    RegisteredOn = "2013-09-22 13=24",
                    Status = "Approved"
                };

                return request.CreateResponse(HttpStatusCode.OK, result);
            });
        }

        public HttpResponseMessage Post(HttpRequestMessage request, dynamic account)
        {
            return CreateHttpResponse(request, () =>
            {
                Guid accountId = account.Id;
                var savedAccount = _accountRepository.Get(accountId);

                //var result = new
                //{
                //    Id = account.Id,
                //    Name = account.Name,
                //    Password = account.Password,
                //    ConsumedStorage = "2938 MB",
                //    NumberOfSpaces = "2",
                //    RegisteredOn = "2013-09-22 13=24",
                //    Status = "Approved"   
                //};

                return request.CreateResponse(HttpStatusCode.OK, savedAccount);
            });
        }
    }
}
