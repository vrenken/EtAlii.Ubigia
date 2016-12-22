namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;
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

        public void Set(IDataContext context, string account, UserSettings settings)
        {
            _logger.Info($"Setting PeopleApi user settings for account: {account}");

            _decoree.Set(context, account, settings);

            _logger.Info($"Finished setting PeopleApi user settings for account: {account}");
        }
    }
}