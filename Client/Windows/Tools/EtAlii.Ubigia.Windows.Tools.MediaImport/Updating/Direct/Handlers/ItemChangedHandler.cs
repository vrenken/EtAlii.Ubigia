namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ItemChangedHandler : IItemUpdateHandler
    {
        private readonly ILogger _logger;
        private readonly IDataContext _context;

        public ItemChangedHandler(
            IDataContext context,
            ILogger logger)
        {
            _logger = logger;
            _context = context;
        }

        public void Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
        }

    }
}
