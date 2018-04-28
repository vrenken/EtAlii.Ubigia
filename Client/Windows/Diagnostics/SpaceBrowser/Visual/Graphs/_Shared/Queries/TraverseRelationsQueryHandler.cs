
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class TraverseRelationsQueryHandler : QueryHandlerBase<TraverseRelationsQuery, Identifier>, ITraverseRelationsQueryHandler
    {
        private readonly IGraphContext _graphContext;

        public TraverseRelationsQueryHandler(
            IGraphContext graphContext)
        {
            _graphContext = graphContext;
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

                        var relatedEntry = _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(id), _graphContext.FindEntryOnGraphQueryHandler).FirstOrDefault();
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
