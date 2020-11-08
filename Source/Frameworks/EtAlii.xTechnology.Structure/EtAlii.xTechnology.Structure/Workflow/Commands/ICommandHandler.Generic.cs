namespace EtAlii.xTechnology.Structure.Workflow
{
    // ReSharper disable once UnusedTypeParameter
    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
    }
}
