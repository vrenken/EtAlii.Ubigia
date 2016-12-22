namespace EtAlii.Servus.Provisioning.Hosting
{
    public interface IProviderHostFactory
    {
        IProviderHost Create(IHostConfiguration configuration);
    }
}
