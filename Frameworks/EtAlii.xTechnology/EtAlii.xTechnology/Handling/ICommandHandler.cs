namespace EtAlii.xTechnology.Structure
{
    public interface ICommandHandler<TParam>
    {
        ICommand<TParam> Create(TParam parameter);
        void Handle(ICommand<TParam> command);
    }

}
