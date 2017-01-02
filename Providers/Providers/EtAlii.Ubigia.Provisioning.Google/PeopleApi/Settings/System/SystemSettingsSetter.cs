namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class SystemSettingsSetter : ISystemSettingsSetter
    {
        public void Set(IDataContext context, SystemSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var script = new[]
            {
                "/Providers += Google/PeopleApi",
                "<= /Providers/Google/PeopleApi <= $settings"
            };

            dynamic settingsToStore = new
            {
                ClientId = settings.ClientId,
                ProjectId = settings.ProjectId,
                AuthenticationUrl = settings.AuthenticationUrl,
                TokenUrl = settings.TokenUrl,
                AuthenticationProviderx509CertificateUrl = settings.AuthenticationProviderx509CertificateUrl,
                ClientSecret = settings.ClientSecret,
                RedirectUrl = settings.RedirectUrl,
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
