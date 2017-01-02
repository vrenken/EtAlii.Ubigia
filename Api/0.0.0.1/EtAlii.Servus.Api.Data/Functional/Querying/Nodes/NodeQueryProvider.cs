namespace EtAlii.Servus.Api.Data
{
    using Remotion.Linq;
    using Remotion.Linq.Parsing.Structure;
    using System.Linq;
    using System.Linq.Expressions;

    // Called by re-linq when a query is to be executed.
    public class NodeQueryProvider : QueryProviderBase
    {
        public NodeQueryProvider(IQueryParser queryParser, IQueryExecutor queryExecutor)
            : base(queryParser, queryExecutor)
        {
        }

        public override IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new Queryable<T>(this, expression);
        } 
    }
}