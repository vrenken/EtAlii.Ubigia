namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    internal class EntryCacheGetRelatedHandler
    {
        private readonly EntryCacheHelper _cacheHelper;
        private readonly EntryCacheGetHandler _entryGetHandler;
        private readonly EntryCacheContextProvider _contextProvider;

        public EntryCacheGetRelatedHandler(
            EntryCacheHelper cacheHelper,
            EntryCacheGetHandler entryGetHandler,
            EntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _entryGetHandler = entryGetHandler;
            _contextProvider = contextProvider;
        }

        public IEnumerable<IReadOnlyEntry> Handle(Identifier identifier, EntryRelation relations)
        {

            var result = new List<IReadOnlyEntry>();

            var entry = _cacheHelper.GetEntry(identifier);
            if (entry == null)
            {
                entry = _contextProvider.Context.Get(identifier);
                _cacheHelper.StoreEntry(entry);
            }

            if (relations.HasFlag(EntryRelation.Child))
            {
                Add(entry.Children, result);
                Add(entry.Children2, result);
            }
            if (relations.HasFlag(EntryRelation.Downdate))
            {
                Add(entry.Downdate, result);
            }
            if (relations.HasFlag(EntryRelation.Index))
            {
                Add(entry.Indexes, result);
            }
            if (relations.HasFlag(EntryRelation.Indexed))
            {
                Add(entry.Indexed, result);
            }
            if (relations.HasFlag(EntryRelation.Next))
            {
                Add(entry.Next, result);
            }
            if (relations.HasFlag(EntryRelation.Parent))
            {
                Add(entry.Parent, result);
                Add(entry.Parent2, result);
            }
            if (relations.HasFlag(EntryRelation.Previous))
            {
                Add(entry.Previous, result);
            }
            if (relations.HasFlag(EntryRelation.Update))
            {
                Add(entry.Updates, result);
            }
            
            return result;
        }

        private void Add(IEnumerable<Relation> relations, List<IReadOnlyEntry> result)
        {
            foreach(var relation in relations)
            {
                Add(relation, result);
            }
        }

        private void Add(Relation relation, List<IReadOnlyEntry> result)
        {
            var entry = _entryGetHandler.Handle(relation.Id);
            result.Add(entry);
        }
    }
}
