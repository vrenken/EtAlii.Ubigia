namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface ISystemConnectionFactory
    {
        ISystemConnection Create(ISystemConnectionConfiguration configuration);
    }
}