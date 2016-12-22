namespace EtAlii.xTechnology.Workflow
{
    public interface ICommandHandler
    {
        void Handle(ICommand command);
    }
}
