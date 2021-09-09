// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;

    public class ScriptProcessorRootUnAssignTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorRootUnAssignTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_UnAssign_Time_Root()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            const string query = "root:time <= ";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            const string arrangeQuery = "root:time <= Object";
            var arrangeScript = _parser.Parse(arrangeQuery, scope).Script;
            var lastSequence = await processor.Process(arrangeScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_UnAssign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            const string query = "root:specialtime <= ";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            const string arrangeQuery = "root:specialtime <= Object";
            var arrangeScript = _parser.Parse(arrangeQuery, scope).Script;
            var lastSequence = await processor.Process(arrangeScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_UnAssign_Object_Root()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            const string query = "root:projects <= ";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            const string arrangeQuery = "root:projects <= Object";
            var arrangeScript = _parser.Parse(arrangeQuery, scope).Script;
            var lastSequence = await processor.Process(arrangeScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
