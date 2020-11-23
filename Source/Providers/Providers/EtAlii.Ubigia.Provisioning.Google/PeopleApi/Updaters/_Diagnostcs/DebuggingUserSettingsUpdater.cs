namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Serilog;

    public class DebuggingUserSettingsUpdater : IUserSettingsUpdater
    {
        private readonly IUserSettingsUpdater _decoree;
        private readonly ILogger _logger = Log.ForContext<IUserSettingsUpdater>();

        public DebuggingUserSettingsUpdater(IUserSettingsUpdater decoree)
        {
            _decoree = decoree;
        }

        public async Task Update(UserSettings userSettings, SystemSettings systemSettings, IGraphSLScriptContext userDataContext, TimeSpan thresholdBeforeExpiration)
        {
            _logger.Debug("Updating: {userSettingsEmail} ({userSettingsUpdated})", userSettings.Email, userSettings.Updated);

            await _decoree.Update(userSettings, systemSettings, userDataContext, thresholdBeforeExpiration).ConfigureAwait(false);

            _logger.Debug("Updated: {userSettingsEmail} ({userSettingsUpdated})", userSettings.Email, userSettings.Updated);
        }
    }
}