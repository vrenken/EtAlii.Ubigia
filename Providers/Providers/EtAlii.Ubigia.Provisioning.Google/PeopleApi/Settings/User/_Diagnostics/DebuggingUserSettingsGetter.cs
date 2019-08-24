namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.Logging;

    public class DebuggingUserSettingsGetter : IUserSettingsGetter
    {
        private readonly IUserSettingsGetter _decoree;
        private readonly ILogger _logger;

        public DebuggingUserSettingsGetter(IUserSettingsGetter decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }


        public async Task<UserSettings[]> Get(IGraphSLScriptContext context)
        {
            _logger.Info($"Getting all PeopleApi user settings");

            var result = await _decoree.Get(context);

            _logger.Info($"Finished getting all PeopleApi user settings: {result.Length}");

            return result;
        }
    }
}