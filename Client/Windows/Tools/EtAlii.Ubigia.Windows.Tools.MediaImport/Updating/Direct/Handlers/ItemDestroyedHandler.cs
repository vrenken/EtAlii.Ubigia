namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    internal class ItemDestroyedHandler : IItemDestroyedHandler
    {
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IStringEscaper _stringEscaper;
        private readonly ILocalPathSplitter _localPathSplitter;

        public ItemDestroyedHandler(
            IStringEscaper stringEscaper, 
            ILocalPathSplitter localPathSplitter, 
            IGraphSLScriptContext scriptContext)
        {
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

            var lastSequence = await _scriptContext.Process("/{0}{1} -= {2}", remoteStart, remotePath, remoteItem);
            await lastSequence.Output.LastOrDefaultAsync();
        }
    }
}
