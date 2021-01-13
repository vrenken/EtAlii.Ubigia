// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;

    internal class TestTraversalParserConfiguration : TraversalParserConfiguration
    {
        public TestTraversalParserConfiguration()
        {
            var diagnostics = DiagnosticsConfiguration.Default;
            this.UseFunctionalDiagnostics(diagnostics)
#if USE_LAPA_PARSER_IN_TESTS
                .UseLapa();
#else
                .UseAntlr();
#endif
        }
    }
}
