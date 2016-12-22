﻿namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;

    public class UserSettingsGetter : IUserSettingsGetter
    {
        public UserSettings[] Get(IDataContext context)
        {
            var script = new[]
            {
                "/Providers += Microsoft/Graph",
                "<= /Providers/Microsoft/Graph/"
            };

            DynamicNode[] result = null;
            var task = Task.Run(async () =>
            {
                var lastSequence = await context.Scripts.Process(script);
                result = lastSequence.Output
                    .ToEnumerable()
                    .Cast<DynamicNode>()
                    .ToArray();
            });
            task.Wait();

            return result.Select(n =>
            {
                var settings = new UserSettings();
                object value;
                if (n.TryGetValue("PrivateKey", out value))
                {
                    settings.PrivateKey = (string)value;
                }
                return settings;
            }).ToArray();
        }
    }
}
