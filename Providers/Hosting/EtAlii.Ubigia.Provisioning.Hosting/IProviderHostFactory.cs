namespace EtAlii.Ubigia.Provisioning.Hosting
{
    public interface IProviderHostFactory
    {
        IProviderHost Create(IHostConfiguration configuration);
    }
}
