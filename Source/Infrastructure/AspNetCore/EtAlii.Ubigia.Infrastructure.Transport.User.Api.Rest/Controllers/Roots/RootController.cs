namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeUri.Data.Api.Roots)]
    public class RootController : RestController
    {
        private readonly IRootRepository _items;

        public RootController(IRootRepository items)
        {
            _items = items;
        }

        /// <summary>
        /// Get all roots for the specified spaceId.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="id"></param>
        /// <param name="idType"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Update Item by id
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="rootId"></param>
        /// <param name="root"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete Item by id
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="rootId"></param>
        /// <returns></returns>
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
