
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure.Workflow;

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
                        yield return id;

                        var relatedEntry = _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(id), _graphContext.FindEntryOnGraphQueryHandler).FirstOrDefault();
                        if (relatedEntry != null)
                        {
                            var relatedRelations = selector(relatedEntry);
                            stack.Push(relatedRelations.Select(relation => relation.Id));
                        }
                    }
                }
            }
        }
    }
}
