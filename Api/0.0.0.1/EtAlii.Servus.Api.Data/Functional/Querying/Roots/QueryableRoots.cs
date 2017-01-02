namespace EtAlii.Servus.Api
{
    using Remotion.Linq;
    using Remotion.Linq.Parsing.Structure;
    using System.Linq;
    using System.Linq.Expressions;

    public class QueryableRoots : QueryableBase<Root>
    {
        public QueryableRoots(IQueryParser parser, IQueryExecutor executor)
            : base(parser, executor)
        {
        }
    }
}