namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsSetter
    {
        void Set(IDataContext context, string account, UserSettings settings);
    }
}