namespace EtAlii.Servus.Infrastructure.Hosting
{
    public interface IHostFactory
    {
        IHost Create(IHostConfiguration configuration);
    }
}