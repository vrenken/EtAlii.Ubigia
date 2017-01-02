namespace EtAlii.Ubigia.Api.Transport
{
    public interface IStorageConnectionFactory
    {
        IStorageConnection Create(IStorageConnectionConfiguration configuration);
    }
}