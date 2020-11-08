namespace EtAlii.xTechnology.Structure.Workflow
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected internal abstract void Handle(TCommand command);

        public void Handle(ICommand command)
        {
            Handle((TCommand)command);
        }
    }
}
