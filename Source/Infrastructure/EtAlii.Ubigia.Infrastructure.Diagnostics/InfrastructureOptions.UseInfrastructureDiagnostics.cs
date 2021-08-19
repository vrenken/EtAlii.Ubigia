// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class InfrastructureOptionsUseInfrastructureDiagnostics
    {
        public static InfrastructureOptions UseInfrastructureDiagnostics(this InfrastructureOptions options)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new DiagnosticsInfrastructureExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
