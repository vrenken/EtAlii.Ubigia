namespace EtAlii.Servus.Infrastructure.WebApi
{
    using EtAlii.xTechnology.Logging;

    [RequiresAuthenticationToken]
    public partial class EntryDataHubProxy : HubProxyBase<EntryDataHub>
    {
        private readonly ILogger _logger;
        private readonly IEntryRepository _items;

        public EntryDataHubProxy(ILogger logger, IEntryRepository items)
        {
            _logger = logger;
            _items = items;
        }
    }
}
