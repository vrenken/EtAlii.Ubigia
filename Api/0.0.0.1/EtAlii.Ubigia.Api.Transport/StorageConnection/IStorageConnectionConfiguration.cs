namespace EtAlii.Ubigia.Api.Transport
{
	public interface IStorageConnectionConfiguration : IConfiguration
    {
        IStorageTransport Transport { get; }
    }
}
