using SimpleInjector;
namespace EtAlii.xTechnology.Workflow
{
    public abstract class CommandBase<TCommandHandler> : ICommand
        where TCommandHandler : class, ICommandHandler
    {
        ICommandHandler ICommand.GetHandler(Container container)
        {
            return container.GetInstance<TCommandHandler>();
        }
    }
}
