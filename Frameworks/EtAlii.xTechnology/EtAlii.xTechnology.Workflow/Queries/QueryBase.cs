namespace EtAlii.xTechnology.Workflow
{
    using SimpleInjector;

    public abstract class QueryBase<TQueryHandler, TResult> : IQuery<TResult>
        where TQueryHandler : class, IQueryHandler<TResult>
    {
        //IQueryHandler<TResult> IQuery<TResult>.GetHandler(Container container)
        //{
        //    return container.GetInstance<TQueryHandler>();
        //}
    }
}
