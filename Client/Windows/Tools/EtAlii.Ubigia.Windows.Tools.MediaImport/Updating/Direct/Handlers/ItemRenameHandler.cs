namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal class ItemRenameHandler : IItemRenameHandler
    {
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IStringEscaper _stringEscaper;
        private readonly ILocalPathSplitter _localPathSplitter;

        public ItemRenameHandler(
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
            _localPathSplitter.Split(localStart, action.OldItem, out var sourceLast, out var sourceRest);

            _localPathSplitter.Split(localStart, action.Item, out var targetLast, out var targetRest);

            var sourceRemoteItem = _stringEscaper.Escape(sourceLast);
            var sourceRemotePath = sourceRest.Any()
                ? $"/{string.Join("/", _stringEscaper.Escape(sourceRest))}"
                : string.Empty;

            var targetRemoteItem = _stringEscaper.Escape(targetLast);
            var targetRemotePath = targetRest.Any()
                ? $"/{string.Join("/", _stringEscaper.Escape(targetRest))}"
                : string.Empty;

            // TODO: This is not a correct rename!
            // We lack the opportunity for now. We need to be able to change the type of the entry for that. 
            var lastSequence = await _scriptContext.Process("$source <= /{0}{1}/{2}\r\n/{0}{1} -= {2}\r\n/{0}{3} += {4}", remoteStart, sourceRemotePath, sourceRemoteItem, targetRemotePath, targetRemoteItem);
            await lastSequence.Output.LastOrDefaultAsync();
        }
    }
}
