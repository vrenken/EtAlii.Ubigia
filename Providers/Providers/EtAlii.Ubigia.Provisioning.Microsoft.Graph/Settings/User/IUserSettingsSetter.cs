namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsSetter
    {
        void Set(IDataContext context, string account, UserSettings settings);
    }
}