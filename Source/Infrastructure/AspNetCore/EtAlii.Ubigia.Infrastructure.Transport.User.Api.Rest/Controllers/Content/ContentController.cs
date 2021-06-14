namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using Microsoft.AspNetCore.Mvc;

    [RequiresAuthenticationToken(Role.User)]
    [Route(RelativeDataUri.Content)]
    public class ContentController : RestController
    {
        private readonly IContentRepository _items;

        public ContentController(IContentRepository items)
        {
            _items = items;
        }

        [HttpGet]
        public async Task<IActionResult> Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId)
        {
            IActionResult response;
            try
            {
                var content = await _items
                    .Get(entryId)
                    .ConfigureAwait(false);
                response = Ok(content);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> Get([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [RequiredFromQuery]ulong contentPartId)
        {
            IActionResult response;
            try
            {
                var contentPart = await _items
                    .Get(entryId, contentPartId)
                    .ConfigureAwait(false);
                response = Ok(contentPart);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new <see cref="ContentDefinition"/> for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [FromBody] Content content)
        {
            IActionResult response;
            try
            {
                // Store the content.
                await _items
                    .Store(entryId, content)
                    .ConfigureAwait(false);

                // Create the response.
                response = Ok();
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Post a new <see cref="ContentPart"/> for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="contentPartId"></param>
        /// <param name="contentPart"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))] Identifier entryId, [RequiredFromQuery] ulong contentPartId, [FromBody] ContentPart contentPart)
        {
            // Remark. We cannot have two post methods at the same time. The hosting
            // framework gets confused and does not out of the box know what method to choose.
            // Even if both have different parameters.
            // It might not be the best fit to alter this in PUT, but as the Rest interface
            // is the least important one this will do for now.
            // We've got bigger fish to fry.
            IActionResult response;
            try
            {
                if (contentPartId != contentPart.Id)
                {
                    throw new InvalidOperationException("ContentPartId does not match");
                }

                // Store the content.
                await _items
                    .Store(entryId, contentPart)
                    .ConfigureAwait(false);

                // Create the response.
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
