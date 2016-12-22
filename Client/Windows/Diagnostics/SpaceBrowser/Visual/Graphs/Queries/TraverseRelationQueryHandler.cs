
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Workflow;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class TraverseRelationQueryHandler : QueryHandlerBase<TraverseRelationQuery, Identifier>
    {
        private readonly IQueryProcessor _queryProcessor;

        public TraverseRelationQueryHandler(IQueryProcessor queryProcessor)  
        {
            _queryProcessor = queryProcessor;
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

                        var relatedEntry = _queryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(id)).FirstOrDefault();
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
