// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Transport.Rest;
    using Microsoft.AspNetCore.Mvc;

    public partial class EntryController
    {
        [HttpGet]
        public async Task<IActionResult> GetSingle([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entry = await _items
                    .Get(entryId, entryRelations)
                    .ConfigureAwait(false);
                response = Ok(entry);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetMultiple([RequiredFromQuery, ModelBinder(typeof(IdentifiersBinder))]IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entries = new List<Entry>();
                foreach (var entryId in entryIds)
                {
                    var entry = await _items
                        .Get(entryId, entryRelations)
                        .ConfigureAwait(false);
                    entries.Add(entry); 
                }
                response = Ok(entries);
            }
            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetRelated([RequiredFromQuery, ModelBinder(typeof(IdentifierBinder))]Identifier entryId, [RequiredFromQuery] EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            IActionResult response;
            try
            {
                var entries = await _items
                    .GetRelated(entryId, entriesWithRelation, entryRelations)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
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
