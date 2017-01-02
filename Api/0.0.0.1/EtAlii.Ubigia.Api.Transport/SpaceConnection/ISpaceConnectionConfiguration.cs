namespace EtAlii.Ubigia.Api.Transport
{
    public interface ISpaceConnectionConfiguration
    {
        ISpaceTransport Transport { get; }

        string Address { get; }
        string AccountName { get; }
        string Password { get; }
        string Space { get; }

        ISpaceConnectionExtension[] Extensions { get; }

        ISpaceConnectionConfiguration Use(ISpaceTransport transport);

        ISpaceConnectionConfiguration Use(string address);
        ISpaceConnectionConfiguration Use(string accountName, string space, string password);

        ISpaceConnectionConfiguration Use(ISpaceConnectionExtension[] extensions);
    }
}
