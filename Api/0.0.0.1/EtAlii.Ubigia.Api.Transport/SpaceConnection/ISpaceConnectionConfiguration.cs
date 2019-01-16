namespace EtAlii.Ubigia.Api.Transport
{
	public interface ISpaceConnectionConfiguration
    {
        ISpaceTransport Transport { get; }

        string Space { get; }

        ISpaceConnectionExtension[] Extensions { get; }

        ISpaceConnectionConfiguration Use(ISpaceTransport transport);
        ISpaceConnectionConfiguration Use(string space);
        ISpaceConnectionConfiguration Use(ISpaceConnectionExtension[] extensions);
    }
}
