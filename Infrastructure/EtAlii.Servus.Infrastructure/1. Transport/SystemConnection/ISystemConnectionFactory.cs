namespace EtAlii.Servus.Infrastructure.Transport
{
    public interface ISystemConnectionFactory
    {
        ISystemConnection Create(ISystemConnectionConfiguration configuration);
    }
}