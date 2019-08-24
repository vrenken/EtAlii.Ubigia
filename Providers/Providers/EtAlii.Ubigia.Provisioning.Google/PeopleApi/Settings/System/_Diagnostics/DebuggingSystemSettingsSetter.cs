namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
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

        public async Task Set(IGraphSLScriptContext context, SystemSettings settings)
        {
            _logger.Info($"Setting PeopleApi system settings");

            await _decoree.Set(context, settings);

            _logger.Info($"Finished setting PeopleApi system settings");
        }
    }
}