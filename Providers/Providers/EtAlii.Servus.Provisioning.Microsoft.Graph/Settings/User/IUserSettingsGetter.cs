namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using EtAlii.Servus.Api.Functional;

    public interface IUserSettingsGetter
    {
        UserSettings[] Get(IDataContext context);
    }
}