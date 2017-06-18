namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    [RequiresAuthenticationToken]
    public partial class EntryController : ApiController
    {
        [Route(RelativeUri.Data.Entry), HttpGet]
        public HttpResponseMessage GetSingle([FromUri(BinderType = typeof(IdentifierBinder), Name = UriParameter.EntryId)]Identifier entryId, EntryRelation entryRelations = EntryRelation.None)
        {
            HttpResponseMessage response;
            try
            {
                var entry = _items.Get(entryId, entryRelations);
                response = Request.CreateResponse(HttpStatusCode.OK, entry);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Entry GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Data.Entries), HttpGet]
        public HttpResponseMessage GetMultiple([FromUri(BinderType = typeof(IdentifiersBinder), Name = UriParameter.EntryIds)]IEnumerable<Identifier> entryIds, EntryRelation entryRelations = EntryRelation.None)
        {
            HttpResponseMessage response;
            try
            {
                var entries = entryIds.Select(entryId => _items.Get(entryId, entryRelations)).ToArray();
                response = Request.CreateResponse(HttpStatusCode.OK, entries);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a Entries GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        [Route(RelativeUri.Data.RelatedEntries), HttpGet]
        public HttpResponseMessage GetRelated([FromUri(BinderType = typeof(IdentifierBinder))]Identifier entryId, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            HttpResponseMessage response;
            try
            {
                var entries = _items.GetRelated(entryId, entriesWithRelation, entryRelations);
                response = Request.CreateResponse(HttpStatusCode.OK, entries);
            }
            catch (Exception ex)
            {
                //_logger.Critical("Unable to serve a ChildEntries GET client request", ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
