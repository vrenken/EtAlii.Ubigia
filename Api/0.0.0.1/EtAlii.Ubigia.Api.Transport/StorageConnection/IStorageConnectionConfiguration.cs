namespace EtAlii.Ubigia.Api.Transport
{
	public interface IStorageConnectionConfiguration : IConfiguration<IStorageConnectionConfiguration>
    {
        IStorageTransport Transport { get; }
    }
}
