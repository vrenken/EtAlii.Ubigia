namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    internal class ItemRenameHandler : IItemUpdateHandler
    {
        private readonly IDataContext _context;
        private readonly StringEscaper _stringEscaper;
        private readonly LocalPathSplitter _localPathSplitter;

        public ItemRenameHandler(
            IDataContext context, 
            StringEscaper stringEscaper, 
            LocalPathSplitter localPathSplitter)
        {
            _context = context;
            _stringEscaper = stringEscaper;
            _localPathSplitter = localPathSplitter;
        }

        public void Handle(ItemCheckAction action, string localStart, string remoteStart)
        {
            string sourceLast;
            string[] sourceRest;
            _localPathSplitter.Split(localStart, action.OldItem, out sourceLast, out sourceRest);

            string targetLast;
            string[] targetRest;
            _localPathSplitter.Split(localStart, action.Item, out targetLast, out targetRest);

            var sourceRemoteItem = _stringEscaper.Escape(sourceLast);
            var sourceRemotePath = sourceRest.Any()
                ? String.Format("/{0}", String.Join("/", _stringEscaper.Escape(sourceRest)))
                : String.Empty;

            var targetRemoteItem = _stringEscaper.Escape(targetLast);
            var targetRemotePath = targetRest.Any()
                ? String.Format("/{0}", String.Join("/", _stringEscaper.Escape(targetRest)))
                : String.Empty;

            // TODO: This is not a correct rename!
            // We lack the opportunity for now. We need to be able to change the type of the entry for that. 
            var task = Task.Run(async () =>
            {
                var lastSequence = await _context.Scripts.Process("$source <= /{0}{1}/{2}\r\n/{0}{1} -= {2}\r\n/{0}{3} += {4}", remoteStart, sourceRemotePath, sourceRemoteItem, targetRemotePath, targetRemoteItem);
                await lastSequence.Output.LastOrDefaultAsync();
            });
            task.Wait();
            
        }
    }
}
