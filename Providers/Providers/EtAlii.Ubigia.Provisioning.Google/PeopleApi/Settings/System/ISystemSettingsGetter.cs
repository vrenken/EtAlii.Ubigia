namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;

    public interface ISystemSettingsGetter
    {
        SystemSettings Get(IDataContext context);
    }
}