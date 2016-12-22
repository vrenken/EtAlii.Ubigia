namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class EntryCacheGetRelatedHandler : IEntryCacheGetRelatedHandler
    {
        private readonly IEntryCacheHelper _cacheHelper;
        private readonly IEntryCacheGetHandler _entryGetHandler;
        private readonly IEntryCacheContextProvider _contextProvider;

        public EntryCacheGetRelatedHandler(
            IEntryCacheHelper cacheHelper,
            IEntryCacheGetHandler entryGetHandler,
            IEntryCacheContextProvider contextProvider)
        {
            _cacheHelper = cacheHelper;
            _entryGetHandler = entryGetHandler;
            _contextProvider = contextProvider;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> Handle(Identifier identifier, EntryRelation relations, ExecutionScope scope)
        {

            var result = new List<IReadOnlyEntry>();

            var entry = _cacheHelper.Get(identifier);
            if (entry == null)
            {
                entry = await _contextProvider.Context.Get(identifier, scope);
                if (_cacheHelper.ShouldStore(entry))
                {
                    _cacheHelper.Store(entry);
                }
            }

            if (relations.HasFlag(EntryRelation.Child))
            {
                await Add(entry.Children, result, scope);
                await Add(entry.Children2, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Downdate))
            {
                await Add(entry.Downdate, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Index))
            {
                await Add(entry.Indexes, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Indexed))
            {
                await Add(entry.Indexed, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Next))
            {
                await Add(entry.Next, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Parent))
            {
                await Add(entry.Parent, result, scope);
                await Add(entry.Parent2, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Previous))
            {
                await Add(entry.Previous, result, scope);
            }
            if (relations.HasFlag(EntryRelation.Update))
            {
                await Add(entry.Updates, result, scope);
            }
            
            return result;
        }

        private async Task Add(IEnumerable<Relation> relations, List<IReadOnlyEntry> result, ExecutionScope scope)
        {
            foreach(var relation in relations)
            {
                await Add(relation, result, scope);
            }
        }

        private async Task Add(Relation relation, List<IReadOnlyEntry> result, ExecutionScope scope)
        {
            if (relation.Id != Identifier.Empty)
            {
                var entry = await _entryGetHandler.Handle(relation.Id, scope);
                result.Add(entry);
            }
        }
    }
}
