namespace EtAlii.Servus.Api.Data
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    internal class PathHelper : IPathHelper
    {
        private readonly IPathExpander _pathExpander;
        private readonly IPathTraverser _pathTraverser;
        private readonly IDataConnection _connection;
        private readonly IPropertiesSerializer _propertiesSerializer;
        private readonly ITimeTraverser _timeTraverser;
        private readonly IContentManager _contentManager;

        public PathHelper(
            IPathExpander pathExpander,
            IPathTraverser pathTraverser, 
            IDataConnection connection,
            IPropertiesSerializer propertiesSerializer,
            ITimeTraverser timeTraverser,
            IContentManager contentManager)
        {
            _pathExpander = pathExpander;
            _pathTraverser = pathTraverser;
            _connection = connection;
            _propertiesSerializer = propertiesSerializer;
            _timeTraverser = timeTraverser;
            _contentManager = contentManager;
        }

        public DynamicNode Get(Path path)
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

        public IReadOnlyEntry GetEntry(Path path)
        {
            Identifier startIdentifier;
            var epandedPath = _pathExpander.Expand(path, out startIdentifier);
            var entry = _pathTraverser.Traverse(epandedPath, startIdentifier);
            return entry;
        }

        public IEnumerable<DynamicNode> GetChildren(Path path)
        {
            var entry = GetEntry(path);
            return _connection.Entries
                .GetRelated(entry.Id, EntryRelation.Child)
                .Select(e => _timeTraverser.Traverse(e))
                .Select(e => new DynamicNode(e));
        }
    }
}
