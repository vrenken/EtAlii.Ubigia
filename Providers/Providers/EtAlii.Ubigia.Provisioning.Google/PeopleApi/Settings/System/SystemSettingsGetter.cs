namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;

    public class SystemSettingsGetter : ISystemSettingsGetter
    {
        public async Task<SystemSettings> Get(IGraphSLScriptContext context)
        {
            var script = new[]
            {
                "/Providers += Google/PeopleApi",
                "<= /Providers/Google/PeopleApi"
            };

            var settings = new SystemSettings();

            var lastSequence = await context.Process(script);
            DynamicNode result = await lastSequence.Output.Cast<DynamicNode>();

            if (result.TryGetValue("ClientId", out var value))
            {
                settings.ClientId = (string)value;
            }

            if (result.TryGetValue("ProjectId", out value))
            {
                settings.ProjectId = (string)value;
            }

            if (result.TryGetValue("AuthenticationUrl", out value))
            {
                settings.AuthenticationUrl = (string)value;
            }

            if (result.TryGetValue("TokenUrl", out value))
            {
                settings.TokenUrl = (string)value;
            }

            if (result.TryGetValue("AuthenticationProviderx509CertificateUrl", out value))
            {
                settings.AuthenticationProviderx509CertificateUrl = (string)value;
            }

            if (result.TryGetValue("ClientSecret", out value))
            {
                settings.ClientSecret = (string)value;
            }

            if (result.TryGetValue("RedirectUrl", out value))
            {
                settings.RedirectUrl = (string)value;
            }

            return settings;
        }
    }
}
