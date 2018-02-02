namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;

    [RoutePrefix(RelativeUri.Admin.AccountsAdministration), CacheWebApi(-1)]
    public class AccountsAdminController : AdminControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsAdminController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int page = 0, int pageSize = 0, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                dynamic[] accounts = null;
                int totalAccounts;

                var allAccounts = _accountRepository
                    .GetAll()
                    .ToArray();

                if (!string.IsNullOrEmpty(filter))
                {
                    
                    var allMatchingAccounts = allAccounts
                        .Where(account => account.Name.ToLower().Contains(filter.ToLower().Trim()));

                    totalAccounts = allMatchingAccounts.Count();

                    accounts = allAccounts
                        .OrderBy(account => account.Name)
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToArray();

                }
                else
                {
                    accounts = allAccounts
                        .OrderBy(m => m.Name)
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToArray();

                    totalAccounts = allAccounts.Length;
                }

                var pagedResult = new
                {
                    Page = page,
                    TotalCount = totalAccounts,
                    TotalPages = (int)Math.Ceiling((decimal)totalAccounts / pageSize),
                    Items = accounts
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedResult);

                return response;
            });
        }
    }
}
