namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Route(RelativeUri.Admin.Api.Accounts)]
    public class AccountController : RestController
    {
        private readonly IAccountRepository _items;

        public AccountController(IAccountRepository items)
        {
            _items = items;
        }

        [HttpGet]
        public IActionResult GetByName([RequiredFromQuery]string accountName)
        {
            IActionResult response;
            try
            {
                var account = _items.Get(accountName);
                response = Ok(account);
            }
            catch (Exception ex)
            {
                //Logger.Critical("Unable to serve a Account GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }


        // Get all Items
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult response;
            try
            {
                var items = _items.GetAll();
                response = Ok(items);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get Item by id
        [HttpGet]
        public IActionResult Get([RequiredFromQuery]Guid accountId)
        {
            IActionResult response;
            try
            {
                var item = _items.Get(accountId);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        [HttpPost]
        public IActionResult Post([FromBody]Account item, string accountTemplate)
        {
            IActionResult response;
            try
            {
                var template = AccountTemplate.All.Single(t => t.Name == accountTemplate);
                item = _items.Add(item, template);
                response = Ok(item);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [HttpPut]
        public IActionResult Put([RequiredFromQuery]Guid accountId, Account account)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(accountId, account);
                response = Ok(result);
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} PUT client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [HttpDelete]
        public IActionResult Delete([RequiredFromQuery]Guid accountId)
        {
            IActionResult response;
            try
            {
                _items.Remove(accountId);
                response = Ok();
            }
            catch (Exception ex)
            {
                //Logger.Warning("Unable to serve a {0} DELETE client request", ex, typeof(T).Name);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }    
}
