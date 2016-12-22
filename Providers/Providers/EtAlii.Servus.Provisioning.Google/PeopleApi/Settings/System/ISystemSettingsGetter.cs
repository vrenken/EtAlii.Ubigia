namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;

    public interface ISystemSettingsGetter
    {
        SystemSettings Get(IDataContext context);
    }
}