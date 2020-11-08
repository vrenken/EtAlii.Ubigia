namespace EtAlii.xTechnology.Structure.Workflow
{
    // ReSharper disable once UnusedTypeParameter
    public interface IUnitOfWorkHandler<TUnitOfWork> : IUnitOfWorkHandler
        where TUnitOfWork : IUnitOfWork
    {
    }
}
