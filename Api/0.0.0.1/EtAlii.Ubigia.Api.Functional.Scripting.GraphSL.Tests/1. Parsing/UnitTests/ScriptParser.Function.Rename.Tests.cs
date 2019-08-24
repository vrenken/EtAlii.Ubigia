namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;
    using Xunit;

    public class ScriptParserFunctionRenameTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionRenameTests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
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