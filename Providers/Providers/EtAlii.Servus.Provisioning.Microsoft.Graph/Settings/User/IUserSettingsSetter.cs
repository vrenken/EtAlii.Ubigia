namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsSetter
    {
        void Set(IDataContext context, string account, UserSettings settings);
    }
}