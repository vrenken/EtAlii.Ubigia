namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class UserSettingsClearer : IUserSettingsClearer
    {
        public void Clear(IDataContext context, string account)
        {
            var script = new[]
            {
                $"/Providers += Google/PeopleApi",
                $"/Providers/Google/PeopleApi -= \"{account}\""
            };

            var task = Task.Run(async () =>
            {
                var lastSequence = await context.Scripts.Process(script);
                await lastSequence.Output.SingleOrDefaultAsync();
            });
            task.Wait();
        }
    }
}
