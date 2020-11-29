namespace EtAlii.xTechnology.Structure.Workflow
{
    using System.Linq;

    public class QueryProcessor : IQueryProcessor
    {
        public IQueryable<TResult> Process<TResult>(IQuery<TResult> query, IQueryHandler<TResult> handler)
        {
            //var handler = query.GetHandler(_container)
            return handler.Handle(query);
        }
    }
}
