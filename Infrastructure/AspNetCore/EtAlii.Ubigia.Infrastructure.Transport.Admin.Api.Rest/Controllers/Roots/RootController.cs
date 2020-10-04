namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.Admin)]
    [Route(RelativeUri.Management.Api.Roots)]
    public class RootController : RestController
    {
        private readonly IRootRepository _items;

        public RootController(IRootRepository items)
        {
            _items = items;
        }


        // Get all spaces for the specified accountid
        [HttpGet]
        public IActionResult Get([RequiredFromQuery]Guid spaceId, [FromQuery(Name = "rootId")]string id, [FromQuery]string idType)
        {
            IActionResult response;
            try
            {
                if (id == null)
                {
                    var roots = _items.GetAll(spaceId);
                    response = Ok(roots);
                }
                else
                {
                    Root root;
                    switch (idType)
                    {
                        case "rootId":
                            var rootId = Guid.Parse(id);
                            root = _items.Get(spaceId, rootId);
                            break;
                        case "rootName":
                            var rootName = id;
                            root = _items.Get(spaceId, rootName);
                            break;
                        default:
                            root = _items.Get(spaceId, id);
                            break;
                    }
                    response = Ok(root);
                }
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        //// Get all spaces for the specified accountid
        //[HttpGet]
        //public IActionResult GetForSpace([RequiredFromQuery]Guid spaceId)
        //[
        //    IActionResult response
        //    try
        //    [
        //        var roots = _items.GetAll(spaceId)
        //        response = Ok(roots)
        //    ]
        //    catch [Exception ex]
        //    [
        //        response = BadRequest(ex.Message)
        //    ]
        //    return response
        //]
        //// Get Item by id
        //[HttpGet]
        //public IActionResult GetByName([RequiredFromQuery]Guid spaceId, [RequiredFromQuery]string rootName)
        //[
        //    IActionResult response
        //    try
        //    [
        //        var root = _items.Get(spaceId, rootName)
        //        response = Ok(root)
        //    ]
        //    catch [Exception ex]
        //    [
        //        response = BadRequest(ex.Message)
        //    ]
        //    return response
        //]
        // Get Item by id
        //[HttpGet]
        //public IActionResult GetById([RequiredFromQuery]Guid spaceId, [RequiredFromQuery]Guid rootId, [RequiredFromQuery]string byId)
        //[
        //    IActionResult response
        //    try
        //    [
        //        var root = _items.Get(spaceId, rootId)
        //        response = Ok(root)
        //    ]
        //    catch[Exception ex]
        //    [
        //        response = BadRequest(ex.Message)
        //    ]
        //    return response
        //]
        // Add item
        [HttpPost]
        public IActionResult Post([RequiredFromQuery]Guid spaceId, [FromBody]Root root)
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
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Update Item by id
        [HttpPut]
        public IActionResult Put([RequiredFromQuery]Guid spaceId, [RequiredFromQuery]Guid rootId, [FromBody]Root root)
        {
            IActionResult response;
            try
            {
                var result = _items.Update(spaceId, rootId, root);

                response = Ok(result);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        // Delete Item by id
        [HttpDelete]
        public IActionResult Delete([RequiredFromQuery]Guid spaceId, [RequiredFromQuery]Guid rootId)
        {
            IActionResult response;
            try
            {
                _items.Remove(spaceId, rootId);

                response = Ok();
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
