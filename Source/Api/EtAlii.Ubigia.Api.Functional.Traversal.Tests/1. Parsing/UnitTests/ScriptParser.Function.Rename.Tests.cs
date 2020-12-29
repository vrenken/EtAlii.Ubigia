namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptParserFunctionRenameTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionRenameTests()
        {
            var diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics);
            _parser = new TestScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Rename()
        {
            // Arrange.
            var scriptText = new[]
            {
                "$v0 <= /Documents/Files+=/Images",
                "$v1 <= rename($v0, 'Photos')",
                "/Documents/Files",
            };

            // Act.
            var script = _parser.Parse(scriptText).Script;

            // Assert.
            Assert.NotNull(script);
            //Assert.NotNull(script)
            //Assert.True(script.Sequences.Count() == 1)
        }
    }
}
