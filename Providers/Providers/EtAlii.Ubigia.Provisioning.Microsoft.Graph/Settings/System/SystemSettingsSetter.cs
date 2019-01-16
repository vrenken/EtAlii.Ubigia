namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class SystemSettingsSetter : ISystemSettingsSetter
    {
        public void Set(IGraphSLScriptContext context, SystemSettings settings)
        {
            var script = new[]
            {
                "/Providers += Microsoft/Graph",
                "/Providers/Microsoft/Graph <= $settings"
            };

            dynamic settingsToStore = new
            {
                ApplicationId = settings.ApplicationId,
                Password = settings.Password,
                PrivateKey = settings.PrivateKey,
            };

            var task = Task.Run(async () =>
            {
                var scope = new ScriptScope();
                scope.Variables.Add("settings", new ScopeVariable(settingsToStore, "Value"));
                var lastSequence = await context.Process(script, scope);
                await lastSequence.Output.SingleOrDefaultAsync();
            });
            task.Wait();
        }
    }
}
