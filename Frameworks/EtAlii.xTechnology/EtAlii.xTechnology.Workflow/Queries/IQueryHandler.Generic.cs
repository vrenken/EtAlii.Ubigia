namespace EtAlii.xTechnology.Workflow
{
    public interface IQueryHandler<TResult, TQuery> : IQueryHandler<TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
