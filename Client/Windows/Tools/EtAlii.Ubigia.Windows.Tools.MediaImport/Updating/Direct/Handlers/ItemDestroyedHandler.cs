namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    internal class ItemDestroyedHandler : IItemUpdateHandler
    {
        private readonly IDataContext _context;
        private readonly StringEscaper _stringEscaper;
        private readonly LocalPathSplitter _localPathSplitter;

        public ItemDestroyedHandler(
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
            string last;
            string[] rest;

            _localPathSplitter.Split(localStart, action.Item, out last, out rest);

            var remoteItem = _stringEscaper.Escape(last);
            var remotePath = rest.Any()
                ? String.Format("/{0}", String.Join("/", _stringEscaper.Escape(rest)))
                : String.Empty;

            var task = Task.Run(async () =>
            {
                var lastSequence = await _context.Scripts.Process("/{0}{1} -= {2}", remoteStart, remotePath, remoteItem);
                await lastSequence.Output.LastOrDefaultAsync();
            });
            task.Wait();
        }
    }
}
