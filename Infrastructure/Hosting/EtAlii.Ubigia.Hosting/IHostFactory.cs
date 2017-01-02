namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    public interface IHostFactory
    {
        IHost Create(IHostConfiguration configuration);
    }
}