namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;

    public class UserSettingsGetter : IUserSettingsGetter
    {
        public UserSettings[] Get(IGraphSLScriptContext context)
        {
            var script = new[]
            {
                "/Providers += Microsoft/Graph",
                "<= /Providers/Microsoft/Graph/"
            };

            DynamicNode[] result = null;
            var task = Task.Run(async () =>
            {
                var lastSequence = await context.Process(script);
                result = lastSequence.Output
                    .ToEnumerable()
                    .Cast<DynamicNode>()
                    .ToArray();
            });
            task.Wait();

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
