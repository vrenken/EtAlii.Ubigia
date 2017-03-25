namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;

    internal class ItemCreatedHandler : IItemCreatedHandler
    {
        private readonly ILogger _logger;
        private readonly IDirectoryHelper _directoryHelper;
        private readonly IDataContext _context;
        private readonly IStringEscaper _stringEscaper;
        private readonly ILocalPathSplitter _localPathSplitter;

        public ItemCreatedHandler(
            IDataContext context, 
            ILogger logger,
            IDirectoryHelper directoryHelper,
            IStringEscaper stringEscaper,
            ILocalPathSplitter localPathSplitter)
        {
            _context = context;
            _logger = logger;
            _directoryHelper = directoryHelper;
            _stringEscaper = stringEscaper;
            _localPathSplitter = localPathSplitter;
        }


        public void Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
            string last;
            string[] rest;

            _localPathSplitter.Split(localStart, action.Item, out last, out rest);

            var remoteItem = _stringEscaper.Escape(last);
            var remotePath = rest.Any()
                ? $"/{String.Join("/", _stringEscaper.Escape(rest))}"
                : String.Empty;

            var task = Task.Run(async () =>
            {
                var lastSequence = await _context.Scripts.Process("/{0}{1} += {2}", remoteStart, remotePath, remoteItem);
                await lastSequence.Output.LastOrDefaultAsync();
            });
            task.Wait();

            var isFile = _directoryHelper.IsFile(action.Item);
            if (isFile)
            {
                // Upload content.
            }
        }
    }
}
