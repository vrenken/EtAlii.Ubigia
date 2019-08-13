
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;

    public class TraverseRelationQueryHandler : QueryHandlerBase<TraverseRelationQuery, Identifier>
    {
        private readonly IGraphContext _graphContext;

        public TraverseRelationQueryHandler(
            IGraphContext graphContext)
        {
            _graphContext = graphContext;
        }

        protected override IQueryable<Identifier> Handle(TraverseRelationQuery query)
        {
            return Traverse(query.Entry, query.Selector).AsQueryable();
        }


        private IEnumerable<Identifier> Traverse(IReadOnlyEntry entry, Func<IReadOnlyEntry, Relation> selector)
        {
            if (entry != null)
            {
                var relation = selector(entry);
                if (relation != Relation.None)
                {
                    var stack = new Stack<Identifier>();
                    stack.Push(relation.Id);

                    while (stack.Count > 0)
                    {
                        var id = stack.Pop();
                        yield return id;

                        var relatedEntry = _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(id), _graphContext.FindEntryOnGraphQueryHandler).FirstOrDefault();
                        if (relatedEntry != null)
                        {
                            var relatedRelation = selector(relatedEntry);
                            if (relatedRelation != Relation.None)
                            {
                                stack.Push(relatedRelation.Id);
                            }
                        }
                    }
                }
            }
        }
    }
}
