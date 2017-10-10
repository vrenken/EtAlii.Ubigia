namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Web.Http;

    public abstract class ApiControllerWithHub<THub, TItems> : ApiController
         where THub : HubProxyBase, new()
        where TItems : class
    {
        protected THub Hub { get { return _hub.Value; } }
        private readonly Lazy<THub> _hub;

        protected ILogger Logger { get { return _logger; } }
        private readonly ILogger _logger;

        protected TItems Items { get { return _items; } }
        private readonly TItems _items;

        protected ApiControllerWithHub(ILogger logger, TItems items)
        {
            _hub = new Lazy<THub>(() => new THub());
            _logger = logger;
            _items = items;
        }
    }
}