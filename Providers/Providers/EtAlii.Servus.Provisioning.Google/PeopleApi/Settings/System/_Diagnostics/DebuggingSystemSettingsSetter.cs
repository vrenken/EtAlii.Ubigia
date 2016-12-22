namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.xTechnology.Logging;

    public class DebuggingSystemSettingsSetter : ISystemSettingsSetter
    {
        private readonly ISystemSettingsSetter _decoree;
        private readonly ILogger _logger;

        public DebuggingSystemSettingsSetter(ISystemSettingsSetter decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public void Set(IDataContext context, SystemSettings settings)
        {
            _logger.Info($"Setting PeopleApi system settings");

            _decoree.Set(context, settings);

            _logger.Info($"Finished setting PeopleApi system settings");
        }
    }
}