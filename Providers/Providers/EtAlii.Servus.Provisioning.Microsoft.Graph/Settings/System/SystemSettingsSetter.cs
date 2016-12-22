namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;

    public class SystemSettingsSetter : ISystemSettingsSetter
    {
        public void Set(IDataContext context, SystemSettings settings)
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
                var lastSequence = await context.Scripts.Process(script, scope);
                await lastSequence.Output.SingleOrDefaultAsync();
            });
            task.Wait();
        }
    }
}
