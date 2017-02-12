namespace EtAlii.xTechnology.Workflow
{
    public class CommandProcessor : ICommandProcessor
    {
        protected virtual void ProcessCommand(ICommand command, ICommandHandler handler)
        {
            handler.Handle(command);
        }

        public void Process(ICommand command, ICommandHandler handler)
        {
            ProcessCommand(command, handler);
        }

    }
}
