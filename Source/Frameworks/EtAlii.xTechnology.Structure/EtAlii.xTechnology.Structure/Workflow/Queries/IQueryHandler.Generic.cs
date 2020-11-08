namespace EtAlii.xTechnology.Structure.Workflow
{
    // ReSharper disable once UnusedTypeParameter
    public interface IQueryHandler<TResult, TQuery> : IQueryHandler<TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
