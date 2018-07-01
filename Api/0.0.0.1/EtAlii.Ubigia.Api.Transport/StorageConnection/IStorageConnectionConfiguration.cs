namespace EtAlii.Ubigia.Api.Transport
{
	public interface IStorageConnectionConfiguration 
    {
        IStorageTransport Transport { get; }
        IStorageConnectionExtension[] Extensions { get; }

        IStorageConnectionConfiguration Use(IStorageTransport transport);
        IStorageConnectionConfiguration Use(IStorageConnectionExtension[] extensions);

    }
}
