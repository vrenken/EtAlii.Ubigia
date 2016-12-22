namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System.Web.Http;
    using EtAlii.Servus.Infrastructure.Functional;

    [RequiresAuthenticationToken]
    public partial class EntryController : ApiController
    {
        private readonly IEntryRepository _items;

        public EntryController(
            IEntryRepository items)
        {
            _items = items;
        }
    }
}
