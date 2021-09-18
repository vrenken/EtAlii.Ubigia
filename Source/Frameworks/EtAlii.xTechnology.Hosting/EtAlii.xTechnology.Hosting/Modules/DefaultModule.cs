// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class DefaultModule : ModuleBase
    {
        private static int _defaultModuleCounter;

        public DefaultModule(IConfigurationSection configuration) : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
        }

        #pragma warning disable S2696 // Pretty sure this counter won't cause any weird threading issues.
        protected override Status CreateInitialStatus() => new($"Module {++_defaultModuleCounter}");
        #pragma warning restore S2696
    }
}
