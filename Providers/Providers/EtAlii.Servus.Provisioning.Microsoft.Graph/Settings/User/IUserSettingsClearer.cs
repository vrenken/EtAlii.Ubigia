namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsClearer
    {
        void Clear(IDataContext context, string account);
    }
}