namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal class ItemCreatedHandler : IItemCreatedHandler
    {
        //private readonly ILogger _logger
        private readonly IDirectoryHelper _directoryHelper;
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IStringEscaper _stringEscaper;
        private readonly ILocalPathSplitter _localPathSplitter;

        public ItemCreatedHandler(
            //ILogger logger,
            IDirectoryHelper directoryHelper,
            IStringEscaper stringEscaper,
            ILocalPathSplitter localPathSplitter, 
            IGraphSLScriptContext scriptContext)
        {
            //_logger = logger
            _directoryHelper = directoryHelper;
            _stringEscaper = stringEscaper;
            _localPathSplitter = localPathSplitter;
            _scriptContext = scriptContext;
        }


        public async Task Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
            _localPathSplitter.Split(localStart, action.Item, out var last, out var rest);

            var remoteItem = _stringEscaper.Escape(last);
            var remotePath = rest.Any()
                ? $"/{string.Join("/", _stringEscaper.Escape(rest))}"
                : string.Empty;

            var lastSequence = await _scriptContext.Process("/{0}{1} += {2}", remoteStart, remotePath, remoteItem);
            await lastSequence.Output.LastOrDefaultAsync();

            var isFile = _directoryHelper.IsFile(action.Item);
            if (isFile)
            {
                // Upload content.
            }
        }
    }
}
