namespace EtAlii.xTechnology.Workflow
{
    using SimpleInjector;

    public class CommandProcessor : ICommandProcessor
    {
        protected Container Container { get { return _container; } }
        private readonly Container _container;

        public CommandProcessor(Container container)
        {
            _container = container;
        }

        protected virtual ICommandHandler GetCommandHandler(Container container, ICommand command)
        {
            return command.GetHandler(_container);
        }

        protected virtual void ProcessCommand(ICommand command, ICommandHandler handler)
        {
            handler.Handle(command);
        }

        public void Process(ICommand command)
        {
            var handler = GetCommandHandler(Container, command);
            ProcessCommand(command, handler);
        }

    }
}
