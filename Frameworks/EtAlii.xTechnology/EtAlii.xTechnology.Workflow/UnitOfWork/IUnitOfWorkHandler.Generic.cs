namespace EtAlii.xTechnology.Workflow
{
    public interface IUnitOfWorkHandler<TUnitOfWork> : IUnitOfWorkHandler
        where TUnitOfWork : IUnitOfWork
    {
    }
}
