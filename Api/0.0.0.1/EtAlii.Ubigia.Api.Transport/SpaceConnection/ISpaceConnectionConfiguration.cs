namespace EtAlii.Ubigia.Api.Transport
{
	using System;

	public interface ISpaceConnectionConfiguration
    {
        ISpaceTransport Transport { get; }

        Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        string Space { get; }

        ISpaceConnectionExtension[] Extensions { get; }

        ISpaceConnectionConfiguration Use(ISpaceTransport transport);

        ISpaceConnectionConfiguration Use(Uri address);
        ISpaceConnectionConfiguration Use(string accountName, string space, string password);

        ISpaceConnectionConfiguration Use(ISpaceConnectionExtension[] extensions);
    }
}
