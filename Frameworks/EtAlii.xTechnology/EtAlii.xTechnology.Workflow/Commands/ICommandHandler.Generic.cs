namespace EtAlii.xTechnology.Workflow
{
    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
    }
}
