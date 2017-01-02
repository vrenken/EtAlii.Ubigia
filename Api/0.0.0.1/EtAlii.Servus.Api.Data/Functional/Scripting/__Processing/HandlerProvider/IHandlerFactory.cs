namespace EtAlii.Servus.Api.Data
{
    public interface IHandlerFactory
    {
        T Create<T>()
            where T : class, IActionHandler;
    }
}
