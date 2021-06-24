// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    public static class InfrastructureConfigurationUseDiagnostics
    {
        public static InfrastructureConfiguration Use(this InfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new DiagnosticsInfrastructureExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}