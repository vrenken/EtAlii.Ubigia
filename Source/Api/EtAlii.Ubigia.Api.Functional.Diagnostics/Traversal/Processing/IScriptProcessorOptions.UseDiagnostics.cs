// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Microsoft.Extensions.Configuration;

    public static class ScriptProcessorOptionsDiagnosticsExtension
    {
        public static TraversalProcessorOptions UseFunctionalDiagnostics(this TraversalProcessorOptions options, IConfiguration configurationRoot)
        {
            var extensions = new IScriptProcessorExtension[]
            {
                new DiagnosticsScriptProcessorExtension(configurationRoot),
            };

            return options.Use(extensions);

        }
    }
}
