namespace EtAlii.Servus.Api.Data
{
    public interface IActionHandler
    {
        void Handle(Action action);
        bool CanHandle(Action action);
    }
}
