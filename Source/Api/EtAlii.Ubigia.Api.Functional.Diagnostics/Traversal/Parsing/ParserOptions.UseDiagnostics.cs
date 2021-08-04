// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class ParserOptionsDiagnosticsExtension
    {
        public static ParserOptions UseFunctionalDiagnostics(this ParserOptions options)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(),
            };

            return options.Use(extensions);

        }
    }
}
