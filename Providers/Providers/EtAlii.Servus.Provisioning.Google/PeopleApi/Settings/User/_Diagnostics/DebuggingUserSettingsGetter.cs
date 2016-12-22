namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;
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


        public UserSettings[] Get(IDataContext context)
        {
            _logger.Info($"Getting all PeopleApi user settings");

            var result = _decoree.Get(context);

            _logger.Info($"Finished getting all PeopleApi user settings: {result.Length}");

            return result;
        }
    }
}