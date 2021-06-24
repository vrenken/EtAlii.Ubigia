// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;

    public static class TraversalParserConfigurationDiagnosticsExtension
    {
        public static TraversalParserConfiguration UseFunctionalDiagnostics(this TraversalParserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(diagnostics),
            };

            return configuration.Use(extensions);

        }
    }
}
