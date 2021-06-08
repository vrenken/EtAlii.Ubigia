// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
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
