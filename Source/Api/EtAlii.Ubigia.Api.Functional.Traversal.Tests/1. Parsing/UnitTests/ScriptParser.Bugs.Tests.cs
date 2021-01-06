// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptParserBugsTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserBugsTests()
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

        [Fact]
        public void ScriptParser_Bugs_001_Object_Assignment()
        {
            // Arrange.
            const string text = "Person:Doe/John <= { Birthdate: 1977-06-27, NickName: 'Johnny', Lives: 1 }";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }
    }
}
