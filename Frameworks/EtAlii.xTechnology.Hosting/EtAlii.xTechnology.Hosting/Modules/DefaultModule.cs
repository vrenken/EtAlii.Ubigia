namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class DefaultModule : ModuleBase
    {
        private static int _defaultModuleCounter;

        public DefaultModule(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
        }

        #pragma warning disable S2696 // Pretty sure this counter won't cause any weird threading issues.
        protected override Status CreateInitialStatus() => new Status($"Module {++_defaultModuleCounter}");
        #pragma warning restore S2696
    }
}