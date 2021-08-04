// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class ScriptProcessorOptionsDiagnosticsExtension
    {
        public static TraversalProcessorOptions UseFunctionalDiagnostics(this TraversalProcessorOptions options)
        {
            var extensions = new IScriptProcessorExtension[]
            {
                new DiagnosticsScriptProcessorExtension(),
            };

            return options.Use(extensions);

        }
    }
}
