using SimpleInjector;
namespace EtAlii.xTechnology.Workflow
{
    public abstract class UnitOfWorkBase<TUnitOfWorkHandler> : IUnitOfWork
        where TUnitOfWorkHandler : class, IUnitOfWorkHandler
    {
        IUnitOfWorkHandler IUnitOfWork.GetHandler(Container container)
        {
            return container.GetInstance<TUnitOfWorkHandler>();
        }
    }
}
