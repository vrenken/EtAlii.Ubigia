namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Serilog;

    public class DebuggingSystemSettingsGetter : ISystemSettingsGetter
    {
        private readonly ISystemSettingsGetter _decoree;
        private readonly ILogger _logger = Log.ForContext<ISystemSettingsGetter>();

        public DebuggingSystemSettingsGetter(ISystemSettingsGetter decoree)
        {
            _decoree = decoree;
        }

        async Task<SystemSettings> ISystemSettingsGetter.Get(IGraphSLScriptContext context)
        {
            _logger.Information("Getting PeopleApi system settings");

            var result = await _decoree.Get(context).ConfigureAwait(false);

            _logger.Information("Finished getting PeopleApi system settings");

            return result;
        }
    }
}