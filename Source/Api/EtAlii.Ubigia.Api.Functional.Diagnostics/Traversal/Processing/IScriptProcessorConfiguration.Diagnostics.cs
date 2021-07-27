// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Microsoft.Extensions.Configuration;

    public static class ScriptProcessorConfigurationDiagnosticsExtension
    {
        public static TraversalProcessorConfiguration UseFunctionalDiagnostics(this TraversalProcessorConfiguration configuration, IConfiguration configurationRoot)
        {
            var extensions = new IScriptProcessorExtension[]
            {
                new DiagnosticsScriptProcessorExtension(configurationRoot),
            };

            return configuration.Use(extensions);

        }
    }
}
