﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorLogicalRemoveTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorLogicalRemoveTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = UbigiaDiagnostics.DefaultConfiguration;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
        }

        public Task DisposeAsync()
        {
            _parser = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            return Task.CompletedTask;
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_1()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName", "SurName");
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
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
            Assert.NotEqual(Identifier.Empty, ((INode)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName", "SurName");
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
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
            Assert.NotEqual(Identifier.Empty, ((INode)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_With_Variable_1()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName", "SurName");
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
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
            Assert.NotEqual(Identifier.Empty, ((INode)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_With_Variable_2()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName", "SurName");
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
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
            Assert.NotEqual(Identifier.Empty, ((INode)beforeResult).Id);
            Assert.Null(afterResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Logical_Remove_With_Variable_3()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var root = await _logicalContext.Roots.Get("Person");
            var entry = await _logicalContext.Nodes.Select(GraphPath.Create(root.Identifier), executionScope);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)entry, "LastName", "SurName");
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
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
            Assert.NotEqual(Identifier.Empty, ((INode)beforeResult).Id);
            Assert.Null(afterResult);
        }
    }
}