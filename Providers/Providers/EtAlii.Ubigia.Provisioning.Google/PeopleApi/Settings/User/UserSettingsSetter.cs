namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class UserSettingsSetter : IUserSettingsSetter
    {
        public Task Set(IGraphSLScriptContext context, string account, UserSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            return SetInternal(context, account, settings);
        }

        private async Task SetInternal(IGraphSLScriptContext context, string account, UserSettings settings)
        {
            var script = new[]
            {
                $"/Providers += Google/PeopleApi/\"{account}\"",
                $"/Providers/Google/PeopleApi/\"{account}\" <= $settings"
            };

            dynamic settingsToStore = new
            {
                Id = settings.Id,
                DisplayName = settings.DisplayName,
                DisplayNameLastFirst = settings.DisplayNameLastFirst,
                FamilyName = settings.FamilyName,
                GivenName = settings.GivenName,
                Email = settings.Email,
                AccessToken = settings.AccessToken,
                RefreshToken = settings.RefreshToken,
                ExpiresIn = settings.ExpiresIn,
                Created = settings.Created,
                Updated = settings.Updated,
                TokenType = settings.TokenType,
            };

            var scope = new ScriptScope();
            scope.Variables.Add("settings", new ScopeVariable(settingsToStore, "Value"));
            var lastSequence = await context.Process(script, scope);
            await lastSequence.Output.SingleOrDefaultAsync();
        }
    }
}
