namespace EtAlii.Servus.Api.Data
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    internal class Path2Helper : IPath2Helper
    {
        private readonly IPath2Expander _pathExpander;
        private readonly IPath2Traverser _pathTraverser;
        private readonly IDataConnection _connection;
        private readonly IPropertiesSerializer _propertiesSerializer;
        private readonly ITime2Traverser _timeTraverser;
        private readonly IContentManager _contentManager;

        public Path2Helper(
            IPath2Expander pathExpander,
            IPath2Traverser pathTraverser, 
            IDataConnection connection,
            IPropertiesSerializer propertiesSerializer,
            ITime2Traverser timeTraverser,
            IContentManager contentManager)
        {
            _pathExpander = pathExpander;
            _pathTraverser = pathTraverser;
            _connection = connection;
            _propertiesSerializer = propertiesSerializer;
            _timeTraverser = timeTraverser;
            _contentManager = contentManager;
        }

        public DynamicNode Get(PathSubject path)
        {
            var entry = GetEntry(path);
            var node = new DynamicNode(entry);
            GetContent(node);
            return node;
        }

        private void GetContent(IInternalNode node)
        {
            using (var stream = new MemoryStream())
            {
                _contentManager.Download(stream, node.Id, false); // TODO: We should validate the checksum.
                stream.Position = 0;
                var properties = _propertiesSerializer.Deserialize<IDictionary<string, object>>(stream);
                node.SetProperties(properties);
            }
        }

        public IReadOnlyEntry GetEntry(PathSubject path)
        {
            var epandedPath = _pathExpander.Expand(path);
            var entry = _pathTraverser.Traverse(epandedPath);
            return entry;
        }

        public IEnumerable<DynamicNode> GetChildren(PathSubject path)
        {
            var entry = GetEntry(path);
            return _connection.Entries
                .GetMultiple(entry.Id, EntryRelation.Child)
                .Select(e => _timeTraverser.Traverse(e))
                .Select(e => new DynamicNode(e));
        }
    }
}
