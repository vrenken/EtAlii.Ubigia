namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public partial class ScriptParser_Function_File_Tests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParser_Function_File_Tests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
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