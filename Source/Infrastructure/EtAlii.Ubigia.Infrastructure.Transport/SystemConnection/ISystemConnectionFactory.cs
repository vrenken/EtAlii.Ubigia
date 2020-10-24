namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface ISystemConnectionFactory
    {
        ISystemConnection Create(ISystemConnectionConfiguration configuration);
    }
}