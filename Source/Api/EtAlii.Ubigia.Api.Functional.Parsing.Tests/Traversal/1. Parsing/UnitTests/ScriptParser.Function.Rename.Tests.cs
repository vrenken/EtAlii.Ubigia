// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;

    public class ScriptParserFunctionRenameTests : IClassFixture<TraversalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionRenameTests(TraversalUnitTestContext testContext)
        {
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
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
