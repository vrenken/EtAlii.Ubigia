namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsClearer
    {
        void Clear(IDataContext context, string account);
    }
}