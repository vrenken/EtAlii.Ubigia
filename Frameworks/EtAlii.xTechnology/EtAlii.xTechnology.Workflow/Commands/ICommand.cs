namespace EtAlii.xTechnology.Workflow
{
    using SimpleInjector;

    public interface ICommand 
    {
        ICommandHandler GetHandler(Container container);
    }
}
