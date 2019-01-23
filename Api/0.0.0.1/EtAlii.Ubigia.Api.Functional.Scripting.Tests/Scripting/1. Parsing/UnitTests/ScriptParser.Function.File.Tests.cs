namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using Xunit;


    public class ScriptParserFunctionFileTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionFileTests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                _parser = null;
            }
        }

        ~ScriptParserFunctionFileTests()
        {
            Dispose(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_File_Add()
        {
            // Arrange.
            //var text = "'SingleLine'";

            // Act.
            //var script = _parser.Parse(text).Script;

            // Assert.
            //Assert.NotNull(script);
            //Assert.True(script.Sequences.Count() == 1);
        }
    }
}