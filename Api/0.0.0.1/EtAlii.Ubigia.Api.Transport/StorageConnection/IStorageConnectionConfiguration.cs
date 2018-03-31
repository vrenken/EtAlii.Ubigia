namespace EtAlii.Ubigia.Api.Transport
{
	using System;

	public interface IStorageConnectionConfiguration 
    {
        IStorageTransport Transport { get; }

        Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        IStorageConnectionExtension[] Extensions { get; }

        IStorageConnectionConfiguration Use(IStorageTransport transport);

        IStorageConnectionConfiguration Use(Uri address);
        IStorageConnectionConfiguration Use(string accountName, string password);

        IStorageConnectionConfiguration Use(IStorageConnectionExtension[] extensions);

    }
}
