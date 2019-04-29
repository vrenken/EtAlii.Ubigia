namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;

    public class UserSettingsGetter : IUserSettingsGetter
    {
        public async Task<UserSettings[]> Get(IGraphSLScriptContext context)
        {
            var script = new[]
            {
                "/Providers += Microsoft/Graph",
                "<= /Providers/Microsoft/Graph/"
            };

            var lastSequence = await context.Process(script);
            var result = await lastSequence.Output
                .Cast<DynamicNode>()
                .ToArray();

            return result.Select(n =>
            {
                var settings = new UserSettings();
                if (n.TryGetValue("PrivateKey", out var value))
                {
                    settings.PrivateKey = (string)value;
                }
                return settings;
            }).ToArray();
        }
    }
}
