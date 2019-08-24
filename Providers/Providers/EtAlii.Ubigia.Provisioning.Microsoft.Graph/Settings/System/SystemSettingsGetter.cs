namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;

    public class SystemSettingsGetter : ISystemSettingsGetter
    {
        public async Task<SystemSettings> Get(IGraphSLScriptContext context)
        {
            var settings = new SystemSettings();

            var script = new[]
            {
                "/Providers += Microsoft/Graph",
                "<= /Providers/Microsoft/Graph"
            };

            var lastSequence = await context.Process(script);
            var result = await lastSequence.Output.Cast<DynamicNode>();

            if (result.TryGetValue("ApplicationId", out var value))
            {
                settings.ApplicationId = (string)value;
            }

            if (result.TryGetValue("Password", out value))
            {
                settings.Password = (string)value;
            }

            if (result.TryGetValue("PrivateKey", out value))
            {
                settings.PrivateKey = (string)value;
            }
            return settings;
        }
    }
}
