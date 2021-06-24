// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Api.Logical
//[
//    using System
//    using System.Collections.Generic
//    using System.Linq
//    using EtAlii.xTechnology.Collections

//    internal class GraphPathChildRelationTraverser2 : IGraphPathChildRelationTraverser
//    [
//        public GraphPathChildRelationTraverser2()
//        [
//        ]
//        public IEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context)
//        [
//            var result = new List<Identifier>()
//            var path = new List<IReadOnlyEntry>()

//            var entry = context.Entries.Get(start)

//            do
//            [
//                path.Add(entry)
//                var entries = context.Entries.GetRelated(entry.Id, EntryRelation.Downdate)
//                if [entries.Multiple[]]
//                [
//                    throw new NotSupportedException("The GraphPathChildRelationTraverser is not able to process splitted temporal paths.")
//                ]
//                entry = entries.SingleOrDefault()

//            ] while [entry ! = null]

//            fo r [int i = path.Count; i > 0; i- -]
//            [
//                entry = path[i - 1]

//                var children = context.Entries.GetRelated(entry.Id, EntryRelation.Child)
//                foreach (var child in children)
//                [
//                    Update(result, child, context)
//                ]
//            ]
//            return result.AsEnumerable()
//        ]
//        private void Update(List<Identifier> list, IReadOnlyEntry entry, ITraversalContext context)
//        [
//            switch [entry.Type]
//            [
//                case EntryType.Add:
//                    list.AddRangeOnce(entry.Children.Select(c => c.Id))
//                    list.AddRangeOnce(entry.Children2.Select(c => c.Id))
//                    break
//                case EntryType.Remove:
//                    Remove(list, entry.Children, context)
//                    Remove(list, entry.Children2, context)
//                    break
//            ]
//        ]
//        private void Remove(List<Identifier> list, IEnumerable<Relation> relations, ITraversalContext context)
//        [
//            var idsToRemove = relations
//                .Select(c => c.Id)
//                .AsEnumerable()
//            foreach (var idToRemove in idsToRemove)
//            [
//                var entry = context.Entries.Get(idToRemove)
//                if [entry.Downdate != Relation.None]
//                [
//                    if [!list.Remove[entry.Downdate.Id]]
//                    [
//                        Remove(list, new Relation[] [ entry.Downdate ], context)
//                    ]
//                ]
//            ]
//        ]
//        //private void Remove(List<Identifier> list, IEnumerable<Relation> relations)
//        //[
//        //    var ids = relations
//        //        .Select(c => c.Id)
//        //        .AsEnumerable()
//        //    var toRemove = ids.SelectMany(id => _context.Fabric.Entries
//        //        .GetRelated(id, EntryRelation.Downdate)
//        //        .Select(c => c.Id))
//        //    list.RemoveRange(toRemove)
//        //]
//    ]
//]