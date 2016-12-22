namespace EtAlii.xTechnology.Workflow
{
    using SimpleInjector;

    public interface IUnitOfWork 
    {
        IUnitOfWorkHandler GetHandler(Container container);
    }
}
