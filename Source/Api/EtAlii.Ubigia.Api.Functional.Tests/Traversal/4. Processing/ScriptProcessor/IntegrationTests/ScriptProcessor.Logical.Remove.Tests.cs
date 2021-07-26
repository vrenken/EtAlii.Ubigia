// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Xunit;

    public class ScriptProcessorLogicalRemoveTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorLogicalRemoveTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_1()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var executionScope = new ExecutionScope(false);
            var root = await logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var removeScript = _parser.Parse("/Person/LastName -= SurName").Script;

            // Act.
            lastSequence = await processor.Process(removeScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(beforeResult);
            Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_2()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var executionScope = new ExecutionScope(false);
            var root = await logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var removeScript = _parser.Parse("/Person/LastName -= /SurName").Script;
            lastSequence = await processor.Process(removeScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(beforeResult);
            Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_With_Variable_1()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var executionScope = new ExecutionScope(false);
            var root = await logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var processor = new TestScriptProcessorFactory().Create(logicalContext, scope);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var removeScript = _parser.Parse("/Person/LastName -= $var").Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
            lastSequence = await processor.Process(removeScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(beforeResult);
            Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_With_Variable_2()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var executionScope = new ExecutionScope(false);
            var root = await logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var processor = new TestScriptProcessorFactory().Create(logicalContext, scope);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var removeScript = _parser.Parse("$var <= \"SurName\"\r\n/Person/LastName -= $var").Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
            lastSequence = await processor.Process(removeScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(beforeResult);
            Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_With_Variable_3()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var executionScope = new ExecutionScope(false);
            var root = await logicalContext.Roots.Get("Person").ConfigureAwait(false);
            var entry = await logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName").ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var processor = new TestScriptProcessorFactory().Create(logicalContext, scope);
            var lastSequence = await processor.Process(selectScript);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var removeScript = _parser.Parse("$var <= /Person/LastName/SurName\r\n/Person/LastName -= $var").Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
            lastSequence = await processor.Process(removeScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(beforeResult);
            Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
            Assert.Null(afterResult);
        }
    }
}
