namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;

    public interface ISystemSettingsSetter
    {
        void Set(IDataContext context, SystemSettings settings);
    }
}