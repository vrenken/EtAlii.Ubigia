namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;

    public class DebuggingUserSettingsSetter : IUserSettingsSetter
    {
        private readonly IUserSettingsSetter _decoree;
        private readonly ILogger _logger;

        public DebuggingUserSettingsSetter(IUserSettingsSetter decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public async Task Set(IGraphSLScriptContext context, string account, UserSettings settings)
        {
            _logger.Info($"Setting PeopleApi user settings for account: {account}");

            await _decoree.Set(context, account, settings);

            _logger.Info($"Finished setting PeopleApi user settings for account: {account}");
        }
    }
}