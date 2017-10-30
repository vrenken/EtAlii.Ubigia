namespace EtAlii.xTechnology.Hosting
{
    public interface IHostFactory
    {
        IHost Create(IHostConfiguration configuration);
    }
}