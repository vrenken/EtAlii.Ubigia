namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class UserSettingsClearer : IUserSettingsClearer
    {
        public async Task Clear(IGraphSLScriptContext context, string account)
        {
            var script = new[]
            {
                $"/Providers += Google/PeopleApi",
                $"/Providers/Google/PeopleApi -= \"{account}\""
            };

            var lastSequence = await context.Process(script);
            await lastSequence.Output.SingleOrDefaultAsync();
        }
    }
}
