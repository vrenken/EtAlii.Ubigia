namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Serilog;

    public class DebuggingUserSettingsSetter : IUserSettingsSetter
    {
        private readonly IUserSettingsSetter _decoree;
        private readonly ILogger _logger = Log.ForContext<IUserSettingsSetter>();

        public DebuggingUserSettingsSetter(IUserSettingsSetter decoree)
        {
            _decoree = decoree;
        }

        public async Task Set(IGraphSLScriptContext context, string account, UserSettings settings)
        {
            _logger.Information("Setting PeopleApi {@UserSettings} for account: {AccountName}", settings, account);

            await _decoree.Set(context, account, settings).ConfigureAwait(false);

            _logger.Information("Finished setting PeopleApi user settings for account: {AccountName}", account);
        }
    }
}