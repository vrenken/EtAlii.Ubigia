namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Path2Traverser : IPath2Traverser
    {
        private readonly IDataConnection _connection;
        private readonly ITime2Traverser _timerTraverser;

        public Path2Traverser(
            IDataConnection connection,
            ITime2Traverser timerTraverser)
        {
            _connection = connection;
            _timerTraverser = timerTraverser;
        }

        public IReadOnlyEntry Traverse(IEnumerable<PathSubjectPart> path)
        {
            var entry = default(IReadOnlyEntry);

            entry = GetRoot(path.OfType<ConstantPathSubjectPart>().First());
            path = path.Skip(1);

            entry = GetRest(path, entry);

            return entry;
        }

        public IReadOnlyEntry GetRest(IEnumerable<PathSubjectPart> path, IReadOnlyEntry entry)
        {
            throw new NotSupportedException("Obsolete code");
            //// Get the entries.
            //foreach (var part in path)
            //{
            //    if (part is IsParentOfPathSubjectPart)
            //    {
            //        var childEntries = _connection.Entries.GetMultiple(entry.Id, EntryRelation.Child);
            //    }
            //    entry = childEntries.SingleOrDefault(e => e.Type == pathComponent);
            //    if (entry == null)
            //    {
            //        var message = String.Format("Unable to traverse path: {0}", String.Join("/", pathComponent));
            //        throw new InvalidOperationException(message);
            //    }

            //    // Also traverse to the latest version of this entry.
            //    entry = _timerTraverser.Traverse(entry);
            //}
            //return entry;
        }

        public IReadOnlyEntry GetRoot(ConstantPathSubjectPart pathSubjectPart)
        {
            var root = _connection.Roots.Get(pathSubjectPart.Name);
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
