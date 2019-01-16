
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Linq;
    using System.Collections.Generic;

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
            var result = new List<Identifier>();

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
                        result.Add(id);

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
            return result;
        }
    }
}
