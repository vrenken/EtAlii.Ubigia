namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsClearer
    {
        void Clear(IDataContext context, string account);
    }
}