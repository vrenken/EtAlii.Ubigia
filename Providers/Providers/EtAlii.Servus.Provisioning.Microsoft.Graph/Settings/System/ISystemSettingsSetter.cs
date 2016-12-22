namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using EtAlii.Servus.Api.Functional;

    public interface ISystemSettingsSetter
    {
        void Set(IDataContext context, SystemSettings settings);
    }
}