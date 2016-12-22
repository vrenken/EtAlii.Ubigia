
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class TraverseRelationsQueryHandler : QueryHandlerBase<TraverseRelationsQuery, Identifier>
    {
        private readonly IQueryProcessor _queryProcessor;

        public TraverseRelationsQueryHandler(IQueryProcessor queryProcessor)  
        {
            _queryProcessor = queryProcessor;
        }

        protected override IQueryable<Identifier> Handle(TraverseRelationsQuery query)
        {
            return Traverse(query.Entry, query.Selector).AsQueryable();
        }

        private IEnumerable<Identifier> Traverse(IReadOnlyEntry entry, Func<IReadOnlyEntry, IEnumerable<Relation>> selector)
        {
            var result = new List<Identifier>();

            if (entry != null)
            {
                var stack = new Stack<IEnumerable<Identifier>>();

                var relations = selector(entry);
                stack.Push(relations.Select(relation => relation.Id));

                while (stack.Count > 0)
                {
                    var ids = stack.Pop();
                    foreach (var id in ids)
                    {
                        result.Add(id);

                        var relatedEntry = _queryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(id)).FirstOrDefault();
                        if (relatedEntry != null)
                        {
                            var relatedRelations = selector(relatedEntry);
                            stack.Push(relatedRelations.Select(relation => relation.Id));
                        }
                    }
                }
            }
            return result;
        }

    }
}
