namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Logging;

    internal class FolderUpdater : IFolderUpdater
    {
        private readonly ILogger _logger;
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IStringEscaper _stringEscaper;

        public FolderUpdater(
            ILogger logger,
            IStringEscaper stringEscaper, 
            IGraphSLScriptContext scriptContext)
        {
            _logger = logger;
            _stringEscaper = stringEscaper;
            _scriptContext = scriptContext;
        }

        public void Handle(string folder, string localStart, string remoteStart)
        {
            var localParts = folder.Split('\\');
            var localStartParts = localStart.Split('\\');

            var localRelativeParts = localParts
                .Skip(localStartParts.Length)
                .ToArray();

            var remotePath = localRelativeParts.Any() ?
                $"{string.Join("/", _stringEscaper.Escape(localRelativeParts))}/"
                : string.Empty;

            DynamicNode[] remoteItems = null;
            var task = Task.Run(async () =>
            {
                var lastSequence = await _scriptContext.Process("/{0}/{1}", remoteStart, remotePath);
                remoteItems = ((IEnumerable<DynamicNode>)await lastSequence.Output).ToArray();
            });
            task.Wait();

            if (remoteItems == null || !remoteItems.Any())
            {
                remoteItems = new DynamicNode[] {};
            }
            var localItems = Directory
                .EnumerateFileSystemEntries(folder)
                .ToArray();

            var itemsToAdd = localItems.Where(li => remoteItems.Any(ri => ri.ToString() == Path.GetFileName(li)));
            var itemsToRemove = remoteItems.Where(ri => localItems.All(li => ri.ToString() != Path.GetFileName(li)));

            foreach (var itemToAdd in itemsToAdd)
            {
                //_itemCreatedHandler.Value.Handle(itemToAdd, localStart, remoteStart)
            }
            foreach (var itemToRemove in itemsToRemove)
            {
                //_itemDestroyedHandler.Value.Handle(itemToRemove.Type, localStart, remoteStart)
            }
        }
    }
}
