namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Serilog;

    public class DebuggingUserSettingsGetter : IUserSettingsGetter
    {
        private readonly IUserSettingsGetter _decoree;
        private readonly ILogger _logger = Log.ForContext<IUserSettingsGetter>();

        public DebuggingUserSettingsGetter(IUserSettingsGetter decoree)
        {
            _decoree = decoree;
        }

        public async Task<UserSettings[]> Get(IGraphSLScriptContext context)
        {
            _logger.Information("Getting all PeopleApi user settings");

            var result = await _decoree.Get(context).ConfigureAwait(false);

            _logger.Information("Finished getting all PeopleApi user settings: {@UserSettings}", result);

            return result;
        }
    }
}