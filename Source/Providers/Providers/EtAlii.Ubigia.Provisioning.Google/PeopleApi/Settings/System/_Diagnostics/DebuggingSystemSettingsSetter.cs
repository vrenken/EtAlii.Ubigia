namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Serilog;

    public class DebuggingSystemSettingsSetter : ISystemSettingsSetter
    {
        private readonly ISystemSettingsSetter _decoree;
        private readonly ILogger _logger = Log.ForContext<ISystemSettingsSetter>();

        public DebuggingSystemSettingsSetter(ISystemSettingsSetter decoree)
        {
            _decoree = decoree;
        }

        public async Task Set(IGraphSLScriptContext context, SystemSettings settings)
        {
            _logger.Information("Setting PeopleApi system settings");

            await _decoree.Set(context, settings).ConfigureAwait(false);

            _logger.Information("Finished setting PeopleApi system settings");
        }
    }
}