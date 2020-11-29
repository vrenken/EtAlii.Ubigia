namespace EtAlii.xTechnology.Structure.Workflow
{
    // ReSharper disable once UnusedTypeParameter
    public abstract class UnitOfWorkBase<TUnitOfWorkHandler> : IUnitOfWork
        where TUnitOfWorkHandler : class, IUnitOfWorkHandler
    {
        //IUnitOfWorkHandler IUnitOfWork.GetHandler(Container container)
        //[
        //    return container.GetInstance<TUnitOfWorkHandler>()
        //]
    }
}
