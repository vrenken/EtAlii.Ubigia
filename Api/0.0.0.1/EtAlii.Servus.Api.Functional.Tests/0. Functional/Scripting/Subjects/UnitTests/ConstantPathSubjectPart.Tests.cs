namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using Xunit;


    public class ConstantPathSubjectPart_Tests : IDisposable
    {
        private IScriptParser _parser;

        public ConstantPathSubjectPart_Tests()
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
        public void ConstantPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new ConstantPathSubjectPart("Test");

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("Test", result);
        }
    }
}