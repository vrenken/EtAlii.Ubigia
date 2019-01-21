namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

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

        public void Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
            _localPathSplitter.Split(localStart, action.OldItem, out var sourceLast, out var sourceRest);

            _localPathSplitter.Split(localStart, action.Item, out var targetLast, out var targetRest);

            var sourceRemoteItem = _stringEscaper.Escape(sourceLast);
            var sourceRemotePath = sourceRest.Any()
                ? $"/{String.Join("/", _stringEscaper.Escape(sourceRest))}"
                : String.Empty;

            var targetRemoteItem = _stringEscaper.Escape(targetLast);
            var targetRemotePath = targetRest.Any()
                ? $"/{String.Join("/", _stringEscaper.Escape(targetRest))}"
                : String.Empty;

            // TODO: This is not a correct rename!
            // We lack the opportunity for now. We need to be able to change the type of the entry for that. 
            var task = Task.Run(async () =>
            {
                var lastSequence = await _scriptContext.Process("$source <= /{0}{1}/{2}\r\n/{0}{1} -= {2}\r\n/{0}{3} += {4}", remoteStart, sourceRemotePath, sourceRemoteItem, targetRemotePath, targetRemoteItem);
                await lastSequence.Output.LastOrDefaultAsync();
            });
            task.Wait();
            
        }
    }
}
