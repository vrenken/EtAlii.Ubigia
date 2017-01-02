namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public interface IStorageConnectionConfiguration 
    {
        IInfrastructureClient Client { get; }
        ITransport Transport { get; }

        string Address { get; }
        string AccountName { get; }
        string Password { get; }
        IStorageConnectionConfiguration Use(ITransport transport);
        IStorageConnectionConfiguration Use(IInfrastructureClient client);

        IStorageConnectionConfiguration Use(string address);
        IStorageConnectionConfiguration Use(string accountName, string password);


    }
}
