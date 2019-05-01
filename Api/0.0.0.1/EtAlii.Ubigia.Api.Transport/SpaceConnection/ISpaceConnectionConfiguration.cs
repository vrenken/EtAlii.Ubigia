namespace EtAlii.Ubigia.Api.Transport
{
	public interface ISpaceConnectionConfiguration : IConfiguration<ISpaceConnectionExtension, SpaceConnectionConfiguration>
    {
        ISpaceTransport Transport { get; }

        string Space { get; }

        ISpaceConnectionConfiguration Use(ISpaceTransport transport);
        ISpaceConnectionConfiguration Use(string space);
    }
}
