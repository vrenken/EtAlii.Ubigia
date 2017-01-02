namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
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
                $"/Providers += Microsoft/Graph",
                $"/Providers/Microsoft/Graph -= \"{account}\""
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
