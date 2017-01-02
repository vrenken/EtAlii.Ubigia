namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsSetter
    {
        void Set(IDataContext context, string account, UserSettings settings);
    }
}