namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public class IsParentOfPathSubjectPart_Tests : IDisposable
    {
        private IScriptParser _parser;

        public IsParentOfPathSubjectPart_Tests()
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
        public void IsParentOfPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new IsParentOfPathSubjectPart();

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("/", result);
        }
    }
}