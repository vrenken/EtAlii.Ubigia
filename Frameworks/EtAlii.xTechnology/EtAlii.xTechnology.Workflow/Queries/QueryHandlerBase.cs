namespace EtAlii.xTechnology.Workflow
{
    using System.Linq;

    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TResult>
        where TQuery : IQuery<TResult>
    {
        protected internal abstract IQueryable<TResult> Handle(TQuery query);

        public IQueryable<TResult> Handle(IQuery<TResult> query)
        {
            return Handle((TQuery)query);
        }
    }
}
