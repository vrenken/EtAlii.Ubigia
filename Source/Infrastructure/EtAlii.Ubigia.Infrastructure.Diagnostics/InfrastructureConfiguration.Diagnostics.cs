// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.Extensions.Configuration;

    public static class InfrastructureConfigurationUseDiagnostics
    {
        public static InfrastructureConfiguration UseInfrastructureDiagnostics(this InfrastructureConfiguration configuration, IConfiguration configurationRoot)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new DiagnosticsInfrastructureExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
