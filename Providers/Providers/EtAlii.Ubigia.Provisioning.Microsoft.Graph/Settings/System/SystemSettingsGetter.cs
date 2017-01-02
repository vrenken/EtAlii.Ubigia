namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;

    public class SystemSettingsGetter : ISystemSettingsGetter
    {
        public SystemSettings Get(IDataContext context)
        {
            var settings = new SystemSettings();

            var script = new[]
            {
                "/Providers += Microsoft/Graph",
                "<= /Providers/Microsoft/Graph"
            };

            DynamicNode result = null;
            var task = Task.Run(async () =>
            {
                var lastSequence = await context.Scripts.Process(script);
                result = await lastSequence.Output.Cast<DynamicNode>();
            });
            task.Wait();

            object value = null;

            if (result.TryGetValue("ApplicationId", out value))
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
