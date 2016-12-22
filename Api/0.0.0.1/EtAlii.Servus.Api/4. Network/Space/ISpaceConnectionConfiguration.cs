namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public interface ISpaceConnectionConfiguration
    {
        IInfrastructureClient Client { get; }
        ITransport Transport { get; }

        string Address { get; }
        string AccountName { get; }
        string Password { get; }
        string Space { get; }

        ISpaceConnectionConfiguration Use(ITransport transport);
        ISpaceConnectionConfiguration Use(IInfrastructureClient client);

        IStorageConnectionConfiguration Use(string address);
        IStorageConnectionConfiguration Use(string accountName, string password, string space);

    }
}
