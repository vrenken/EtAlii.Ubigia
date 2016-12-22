namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using EtAlii.Servus.Api.Functional;

    public interface ISystemSettingsGetter
    {
        SystemSettings Get(IDataContext context);
    }
}