namespace EtAlii.Servus.Api.Data
{
    public interface IConnectionStatusProvider
    {
        IDataConnection Connection {get; set; }
        bool IsConnected { get; }
    }
}
