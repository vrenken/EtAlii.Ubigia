﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using Xunit;

    public class VariablePathSubjectPartTests : IDisposable
    {
        private IScriptParser _parser;

        public VariablePathSubjectPartTests()
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
        public void VariablePathSubjectPart_ToString()
        {
            // Arrange.
            var part = new VariablePathSubjectPart("Test");

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("$Test", result);
        }
    }
}