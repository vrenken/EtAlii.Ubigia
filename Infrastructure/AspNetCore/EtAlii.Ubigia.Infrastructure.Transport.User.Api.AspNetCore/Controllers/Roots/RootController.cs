namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public class RootController : WebApiController
    {
        private readonly IRootRepository _items;

        public RootController(IRootRepository items)
        {
            _items = items;
        }

        // Get all spaces for the specified accountid
        [Route(RelativeUri.Data.Roots + "/{spaceId}"), HttpGet]
        public IActionResult GetForSpace(Guid spaceId)
        {
            IActionResult response;
            try
            {
                var roots = _items.GetAll(spaceId);
                response = Ok(roots);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }


        // Get Item by id
        [Route(RelativeUri.Data.Roots + "/{spaceId}/{rootId}"), HttpGet]
        public IActionResult GetById(Guid spaceId, Guid rootId)
        {
            IActionResult response;
            try
            {
                var root = _items.Get(spaceId, rootId);
                response = Ok(root);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Get Item by id
        [Route(RelativeUri.Data.Roots + "/{spaceId}/{rootName}"), HttpGet]
        public IActionResult GetByName(Guid spaceId, string rootName)
        {
            IActionResult response;
            try
            {
                var root = _items.Get(spaceId, rootName);
                response = Ok(root);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Add item
        [Route(RelativeUri.Data.Roots + "/{spaceId}"), HttpPost]
        public IActionResult Post(Guid spaceId, [FromBody]Root root)
        {
            IActionResult response;
            try
            {
                root = _items.Add(spaceId, root);

                if (root == null)
                {
                    throw new InvalidOperationException("Unable to add root");
                }

                response = Ok(root);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root POST client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [Route(RelativeUri.Data.Roots + "/{spaceId}"), HttpPut]
        public IActionResult Put(Guid spaceId, Guid rootId, Root root)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(spaceId, rootId, root);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root PUT client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [Route(RelativeUri.Data.Roots + "/{spaceId}/{rootId}"), HttpDelete]
        public IActionResult Delete(Guid spaceId, Guid rootId)
        {
            IActionResult response;
            try
            {
                _items.Remove(spaceId, rootId);

                response = Ok();
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Root DELETE client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
