namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsGetter
    {
        UserSettings[] Get(IDataContext context);
    }
}