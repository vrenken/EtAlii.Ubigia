namespace EtAlii.Ubigia.Provisioning
{
    public interface IProviderConfigurationsSection
    {
        IProviderConfiguration[] ToProviderConfigurations();
    }
}