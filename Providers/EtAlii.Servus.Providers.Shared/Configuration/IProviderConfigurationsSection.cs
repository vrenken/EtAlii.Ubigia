namespace EtAlii.Servus.Provisioning
{
    public interface IProviderConfigurationsSection
    {
        IProviderConfiguration[] ToProviderConfigurations();
    }
}