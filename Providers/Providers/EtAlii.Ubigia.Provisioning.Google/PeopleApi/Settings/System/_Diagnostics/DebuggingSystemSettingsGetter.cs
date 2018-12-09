namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;

    public class DebuggingSystemSettingsGetter : ISystemSettingsGetter
    {
        private readonly ISystemSettingsGetter _decoree;
        private readonly ILogger _logger;

        public DebuggingSystemSettingsGetter(ISystemSettingsGetter decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        SystemSettings ISystemSettingsGetter.Get(IGraphSLScriptContext context)
        {
            _logger.Info($"Getting PeopleApi system settings");

            var result = _decoree.Get(context);

            _logger.Info($"Finished getting PeopleApi system settings");

            return result;
        }
    }
}