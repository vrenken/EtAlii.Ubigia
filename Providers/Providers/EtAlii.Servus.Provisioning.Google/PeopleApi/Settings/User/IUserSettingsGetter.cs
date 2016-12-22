namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsGetter
    {
        UserSettings[] Get(IDataContext context);
    }
}