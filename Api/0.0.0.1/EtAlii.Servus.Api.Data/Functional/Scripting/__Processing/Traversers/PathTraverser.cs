namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class PathTraverser : IPathTraverser
    {
        private readonly IDataConnection _connection;
        private readonly ITimeTraverser _timerTraverser;

        public PathTraverser(
            IDataConnection connection,
            ITimeTraverser timerTraverser)
        {
            _connection = connection;
            _timerTraverser = timerTraverser;
        }

        public IReadOnlyEntry Traverse(IEnumerable<string> path, Identifier startIdentifier)
        {
            var entry = default(IReadOnlyEntry);

            if (startIdentifier == Identifier.Empty)
            {
                entry = GetRoot(path.First());
                path = path.Skip(1);
            }
            else
            {
                entry = GetEntry(startIdentifier);
            }

            entry = GetRest(path, entry);

            return entry;
        }

        public IReadOnlyEntry GetRest(IEnumerable<string> path, IReadOnlyEntry entry)
        {
            // Get the entries.
            foreach (var pathComponent in path)
            {
                var childEntries = _connection.Entries.GetRelated(entry.Id, EntryRelation.Child);
                entry = childEntries.SingleOrDefault(e => e.Type == pathComponent);
                if (entry == null)
                {
                    var message = String.Format("Unable to traverse path: {0}", String.Join("/", pathComponent));
                    throw new InvalidOperationException(message);
                }

                // Also traverse to the latest version of this entry.
                entry = _timerTraverser.Traverse(entry);
            }
            return entry;
        }

        public IReadOnlyEntry GetRoot(string rootName)
        {
            var root = _connection.Roots.Get(rootName);
            var entry = _connection.Entries.Get(root.Identifier);
            
            // Also traverse to the latest version of this entry.
            entry = _timerTraverser.Traverse(entry);
            
            return entry;
        }


        private IReadOnlyEntry GetEntry(Identifier identifier)
        {
            var entry = _connection.Entries.Get(identifier);
            
            // Also traverse to the latest version of this entry.
            entry = _timerTraverser.Traverse(entry);

            return entry;
        }
    }
}
