namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.Diagnostics;

    public class DebuggingUserSettingsUpdater : IUserSettingsUpdater
    {
        private readonly IUserSettingsUpdater _decoree;
        private readonly ILogger _logger;

        public DebuggingUserSettingsUpdater(IUserSettingsUpdater decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public async Task Update(UserSettings userSettings, SystemSettings systemSettings, IGraphSLScriptContext userDataContext, TimeSpan thresholdBeforeExpiration)
        {
            _logger.Info($"Updating: {userSettings.Email} ({userSettings.Updated})");

            await _decoree.Update(userSettings, systemSettings, userDataContext, thresholdBeforeExpiration);

            _logger.Info($"Updated: {userSettings.Email} ({userSettings.Updated})");
        }
    }
}