namespace EtAlii.Servus.Api.Transport
{
    public interface IStorageConnectionFactory
    {
        IStorageConnection Create(IStorageConnectionConfiguration configuration);
    }
}