namespace EtAlii.Servus.Provisioning
{
    public interface IProviderFactory
    {
        IProvider Create(IProviderConfiguration configuration);
    }
}
