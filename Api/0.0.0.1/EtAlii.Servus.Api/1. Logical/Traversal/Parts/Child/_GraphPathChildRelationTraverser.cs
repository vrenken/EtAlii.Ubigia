//namespace EtAlii.Servus.Api.Logical
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using EtAlii.xTechnology.Collections;

//    internal class GraphPathChildRelationTraverser : IGraphPathChildRelationTraverser
//    {
//        private readonly ITraversalContext _context;
//        private readonly IGraphPathOriginalRelationTraverser _graphPathOriginalRelationTraverser;

//        public GraphPathChildRelationTraverser(
//            ITraversalContext context,
//            IGraphPathOriginalRelationTraverser graphPathOriginalRelationTraverser)
//        {
//            _context = context;
//            _graphPathOriginalRelationTraverser = graphPathOriginalRelationTraverser;
//        }

//        public IEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start)
//        {
//            // TODO: this should be done differently:
//            // Get all downdates until null.
//            // reverse them.
//            // convert them into a list by checking the adds and removals.

//            var starts = _context.Fabric.Entries
//                .GetRelated(start, EntryRelation.Child)
//                .AsEnumerable();

//            var origins = starts
//                .Select(e => _graphPathOriginalRelationTraverser.Traverse(part, e.Id).Single())
//                .AsEnumerable();

//            if (origins.Multiple())
//            {
//                throw new NotSupportedException("The GraphPathChildRelationTraverser is not able to process multiple origins.");
//            }

//            var list = new List<Identifier>();
//            var entryId = origins.SingleOrDefault(); // We cannot handle multiple updates yet.
//            if(entryId != Identifier.Empty)
//            { 
//                IReadOnlyEntry entry;
//                do
//                {
//                    entry = _context.Fabric.Entries.Get(entryId);
//                    if (entry.Updates.Multiple())
//                    {
//                        throw new NotSupportedException("This path can not be traversed using the GraphPathChildRelationTraverser.");
//                    }

//                    Update(list, entry);
//                    var nextEntry = entry.Updates.SingleOrDefault(); // We cannot handle multiple updates yet.
//                    if (nextEntry != Relation.None)
//                    {
//                        entryId = nextEntry.Id;
//                    }
//                }
//                while (entry.Updates.Any() && !starts.Contains(entry));
//            }
//            return list.AsEnumerable();

//            //return links.Select(l => _context.Fabric.Entries
//            //    .GetRelated(l, EntryRelation.Child)
//            //    .Select(e => e.Id))
//            //    .SelectMany(ends => ends)
//            //    .AsEnumerable();
//        }

//        private void Update(List<Identifier> list, IReadOnlyEntry entry)
//        {
//            // TODO: should we remove all the downdates of a item as well?
//            switch (entry.Type)
//            {
//                case null:
//                    list.Clear();
//                    break;
//                case EntryType.Add:
//                    list.AddRangeOnce(entry.Children.Select(c => c.Id));
//                    list.AddRangeOnce(entry.Children2.Select(c => c.Id));
//                    break;
//                case EntryType.Remove:
//                    Remove(list, entry.Children);
//                    Remove(list, entry.Children2);
//                    break;
//                default:
//                    throw new NotSupportedException("The GraphPathChildRelationTraverser is unable to process the specified link type.");
//            }
//        }

//        private void Remove(List<Identifier> list, IEnumerable<Relation> relations)
//        {
//            var idsToRemove = relations
//                .Select(c => c.Id)
//                .AsEnumerable();
//            foreach (var idToRemove in idsToRemove)
//            {
//                var entry = _context.Fabric.Entries.Get(idToRemove);
//                if (entry.Downdate != Relation.None)
//                {
//                    if (!list.Remove(entry.Downdate.Id))
//                    {
//                        Remove(list, new Relation[] { entry.Downdate } );
//                    }
//                }
//            }
//        }

//        //private void Remove(List<Identifier> list, IEnumerable<Relation> relations)
//        //{
//        //    var ids = relations
//        //        .Select(c => c.Id)
//        //        .AsEnumerable();
//        //    var toRemove = ids.SelectMany(id => _context.Fabric.Entries
//        //        .GetRelated(id, EntryRelation.Downdate)
//        //        .Select(c => c.Id));
//        //    list.RemoveRange(toRemove);
//        //}

//    }
//}