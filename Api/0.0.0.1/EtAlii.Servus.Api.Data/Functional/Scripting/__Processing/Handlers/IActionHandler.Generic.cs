namespace EtAlii.Servus.Api.Data
{
    public interface IActionHandler<T> : IActionHandler
        where T: Action
    {
        void Handle(T action);
    }
}
