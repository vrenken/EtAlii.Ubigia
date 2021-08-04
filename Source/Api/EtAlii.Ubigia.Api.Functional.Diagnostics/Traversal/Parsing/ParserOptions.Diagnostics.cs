// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Microsoft.Extensions.Configuration;

    public static class ParserOptionsDiagnosticsExtension
    {
        public static ParserOptions UseFunctionalDiagnostics(this ParserOptions options, IConfiguration configurationRoot)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(configurationRoot),
            };

            return options.Use(extensions);

        }
    }
}
