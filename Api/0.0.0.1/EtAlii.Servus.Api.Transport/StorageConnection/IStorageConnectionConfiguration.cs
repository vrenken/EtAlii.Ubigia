namespace EtAlii.Servus.Api.Transport
{
    public interface IStorageConnectionConfiguration 
    {
        IStorageTransport Transport { get; }

        string Address { get; }
        string AccountName { get; }
        string Password { get; }
        IStorageConnectionExtension[] Extensions { get; }

        IStorageConnectionConfiguration Use(IStorageTransport transport);

        IStorageConnectionConfiguration Use(string address);
        IStorageConnectionConfiguration Use(string accountName, string password);

        IStorageConnectionConfiguration Use(IStorageConnectionExtension[] extensions);

    }
}
