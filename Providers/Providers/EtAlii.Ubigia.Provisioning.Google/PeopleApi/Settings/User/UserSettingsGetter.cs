namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;

    public class UserSettingsGetter : IUserSettingsGetter
    {
        public async Task<UserSettings[]> Get(IGraphSLScriptContext context)
        {
            var script = new[]
            {
                "/Providers += Google/PeopleApi",
                "<= /Providers/Google/PeopleApi/"
            };

            var lastSequence = await context.Process(script);
            DynamicNode[] result = lastSequence.Output
                .ToEnumerable()
                .Cast<DynamicNode>()
                .ToArray();

            return result.Select(n =>
            {
                var settings = new UserSettings();

                TryGetValueAndSet<string>(n, "Id", value => settings.Id = value);
                TryGetValueAndSet<string>(n, "DisplayName", value => settings.DisplayName = value);
                TryGetValueAndSet<string>(n, "DisplayNameLastFirst", value => settings.DisplayNameLastFirst = value);
                TryGetValueAndSet<string>(n, "FamilyName", value => settings.FamilyName = value);
                TryGetValueAndSet<string>(n, "GivenName", value => settings.GivenName = value);
                TryGetValueAndSet<string>(n, "Email", value => settings.Email = value);
                TryGetValueAndSet<string>(n, "AccessToken", value => settings.AccessToken = value);
                TryGetValueAndSet<string>(n, "RefreshToken", value => settings.RefreshToken = value);
                TryGetValueAndSet<string>(n, "TokenType", value => settings.TokenType = value);
                TryGetValueAndSet<TimeSpan>(n, "ExpiresIn", value => settings.ExpiresIn = value);
                TryGetValueAndSet<DateTime>(n, "Created", value => settings.Created = value);
                TryGetValueAndSet<DateTime>(n, "Updated", value => settings.Updated = value);

                return settings;
            }).ToArray();
        }

        private void TryGetValueAndSet<T>(DynamicNode n, string key, Action<T> set)
        {
            if (n.TryGetValue(key, out var value))
            {
                set((T)value);
            }
        }
    }
}
