namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.NetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Transport.NetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[RequiresAuthenticationToken]
    [Authorize]
    public partial class EntryController
    {
        [HttpGet]
        public IActionResult GetSingle([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entry = _items.Get(entryId, entryRelations);
                response = Ok(entry);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet]
        public IActionResult GetMultiple([RequiredFromQuery, ModelBinder(typeof(IdentifiersBinder))]IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entries = entryIds.Select(entryId => _items.Get(entryId, entryRelations)).ToArray();
                response = Ok(entries);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet]
        //public IActionResult GetRelated([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        public IActionResult GetRelated([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [RequiredFromQuery] EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entries = _items.GetRelated(entryId, entriesWithRelation, entryRelations);
                response = Ok(entries);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }
    }
}
