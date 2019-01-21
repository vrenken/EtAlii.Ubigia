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
        public UserSettings[] Get(IGraphSLScriptContext context)
        {
            var script = new[]
            {
                "/Providers += Google/PeopleApi",
                "<= /Providers/Google/PeopleApi/"
            };

            DynamicNode[] result = null;
            var task = Task.Run(async () =>
            {
                var lastSequence = await context.Process(script);
                result = lastSequence.Output
                    .ToEnumerable()
                    .Cast<DynamicNode>()
                    .ToArray();
            });
            task.Wait();

            return result.Select(n =>
            {
                var settings = new UserSettings();
                if (n.TryGetValue("Id", out var value))
                {
                    settings.Id = (string)value;
                }
                if (n.TryGetValue("DisplayName", out value))
                {
                    settings.DisplayName = (string)value;
                }
                if (n.TryGetValue("DisplayNameLastFirst", out value))
                {
                    settings.DisplayNameLastFirst = (string)value;
                }
                if (n.TryGetValue("FamilyName", out value))
                {
                    settings.FamilyName = (string)value;
                }
                if (n.TryGetValue("GivenName", out value))
                {
                    settings.GivenName = (string)value;
                }
                if (n.TryGetValue("Email", out value))
                {
                    settings.Email = (string)value;
                }
                if (n.TryGetValue("AccessToken", out value))
                {
                    settings.AccessToken = (string)value;
                }
                if (n.TryGetValue("RefreshToken", out value))
                {
                    settings.RefreshToken = (string)value;
                }
                if (n.TryGetValue("TokenType", out value))
                {
                    settings.TokenType = (string)value;
                }
                if (n.TryGetValue("ExpiresIn", out value))
                {
                    settings.ExpiresIn = (TimeSpan)value;
                }
                if (n.TryGetValue("Created", out value))
                {
                    settings.Created = (DateTime)value;
                }
                if (n.TryGetValue("Updated", out value))
                {
                    settings.Updated = (DateTime)value;
                }
                return settings;
            }).ToArray();
        }
    }
}
