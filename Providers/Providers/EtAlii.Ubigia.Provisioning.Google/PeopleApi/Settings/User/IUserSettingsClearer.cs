namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsClearer
    {
        Task Clear(IGraphSLScriptContext context, string account);
    }
}