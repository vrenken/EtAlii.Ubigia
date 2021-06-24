// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.Diagnostics;

    internal class TestScriptParserFactory : ScriptParserFactory
    {
        public IScriptParser Create()
        {
            var diagnostics = DiagnosticsConfiguration.Default;
            var configuration = new TraversalParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics)
                .UseTestParser();

            return Create(configuration);
        }
    }
}
