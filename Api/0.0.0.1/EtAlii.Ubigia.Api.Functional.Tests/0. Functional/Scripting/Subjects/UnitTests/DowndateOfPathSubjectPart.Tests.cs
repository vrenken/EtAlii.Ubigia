namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public class DowndateOfPathSubjectPart_Tests : IDisposable
    {
        private IScriptParser _parser;

        public DowndateOfPathSubjectPart_Tests()
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
        public void PreviousSubjectPart_ToString()
        {
            // Arrange.
            var part = new DowndateOfPathSubjectPart();

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("{", result);
        }
    }
}