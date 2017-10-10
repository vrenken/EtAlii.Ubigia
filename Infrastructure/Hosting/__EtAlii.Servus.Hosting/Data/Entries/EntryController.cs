namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.xTechnology.Logging;
    using System.Web.Http;

    [RequiresAuthenticationToken]
    public partial class EntryController : ApiController
    {
        private readonly EntryHubServerProxy _hub;
        private readonly ILogger _logger;
        private readonly IEntryRepository _items;

        public EntryController(
            ILogger logger, 
            IEntryRepository items,
            EntryHubServerProxy hub)
        {
            _hub = hub;
            _logger = logger;
            _items = items;
        }
    }
}
