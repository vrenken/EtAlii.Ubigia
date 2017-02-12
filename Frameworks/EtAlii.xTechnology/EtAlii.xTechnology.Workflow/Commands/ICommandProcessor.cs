namespace EtAlii.xTechnology.Workflow
{
    public interface ICommandProcessor
    {
        void Process(ICommand command, ICommandHandler handler);
    }
}