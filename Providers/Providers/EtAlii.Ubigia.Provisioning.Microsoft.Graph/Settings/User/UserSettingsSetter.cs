namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class UserSettingsSetter : IUserSettingsSetter
    {
        public void Set(IGraphSLScriptContext context, string account, UserSettings settings)
        {
            var script = new[]
            {
                $"/Providers += Microsoft/Graph/\"{account}\"",
                $"/Providers/Microsoft/Graph/\"{account}\" <= $settings"
            };

            dynamic settingsToStore = new
            {
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
