namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class UserSettingsClearer : IUserSettingsClearer
    {
        public async Task Clear(IGraphSLScriptContext context, string account)
        {
            var script = new[]
            {
                $"/Providers += Microsoft/Graph",
                $"/Providers/Microsoft/Graph -= \"{account}\""
            };

            var lastSequence = await context.Process(script);
            await lastSequence.Output.SingleOrDefaultAsync();
        }
    }
}
