namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Transport.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public partial class EntryController : WebApiController
    {
        [Route(RelativeUri.Data.Entry + "/{" + UriParameter.EntryId + "}"), HttpGet]
        //public IActionResult GetSingle([FromUri(BinderType = typeof(IdentifierBinder), Name = UriParameter.EntryId)]Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        public IActionResult GetSingle([ModelBinder(typeof(IdentifierBinder))]Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entry = _items.Get(entryId, entryRelations);
                response = Ok(entry);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Entry GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Data.Entries + "/{" + UriParameter.EntryIds + "}"), HttpGet]
        //public IActionResult GetMultiple([FromUri(BinderType = typeof(IdentifiersBinder), Name = UriParameter.EntryIds)]IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        public IActionResult GetMultiple([ModelBinder(typeof(IdentifiersBinder))]IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entries = entryIds.Select(entryId => _items.Get(entryId, entryRelations)).ToArray();
                response = Ok(entries);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Entries GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Data.RelatedEntries + "/{entryId}"), HttpGet]
        //public IActionResult GetRelated([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        public IActionResult GetRelated([ModelBinder(typeof(IdentifierBinder))]Identifier entryId, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entries = _items.GetRelated(entryId, entriesWithRelation, entryRelations);
                response = Ok(entries);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ChildEntries GET client request", ex);
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
