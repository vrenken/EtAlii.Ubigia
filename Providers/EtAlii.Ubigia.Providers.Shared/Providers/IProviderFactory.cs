namespace EtAlii.Ubigia.Provisioning
{
    public interface IProviderFactory
    {
        IProvider Create(IProviderConfiguration configuration);
    }
}
