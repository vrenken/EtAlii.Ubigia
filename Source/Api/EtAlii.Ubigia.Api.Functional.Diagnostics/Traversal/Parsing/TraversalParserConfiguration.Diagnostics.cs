// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Microsoft.Extensions.Configuration;

    public static class TraversalParserConfigurationDiagnosticsExtension
    {
        public static TraversalParserConfiguration UseFunctionalDiagnostics(this TraversalParserConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(configurationRoot),
            };

            return configuration.Use(extensions);

        }
    }
}
