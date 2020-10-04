﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathGetTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathGetTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _parser = null;
            return Task.CompletedTask;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItem()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "/Time";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByVariable_1()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "$var1 <= /Time",
                "$var1"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<DynamicNode>(result.Single());
            Assert.Equal("Time", result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByVariable_2()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                "$var1 <= /\"Time\"",
                "$var1"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<DynamicNode>(result.Single());
            Assert.Equal("Time", result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByVariables_1()
        {
            // Arrange.
            var continent = "Europe";
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                $"/Location+={continent}",
                "$var1 <= Location",
                $"$var2 <= {continent}",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByVariables_2()
        {
            // Arrange.
            var continent = "Europe";
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                $"/Location+={continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByVariables_Spaced()
        {
            // Arrange.
            var continent = "Europe";
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                $"/Location += {continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByCompositeVariable()
        {
            // Arrange.
            var continent = "Europe";
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                $"/Location+={continent}",
                $"$var1 <= \"Location/{continent}\"",
                "/$var1"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Get_GetItemByCompositeVariable_Spaced()
        {
            // Arrange.
            var continent = "Europe";
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var queries = new[]
            {
                $"/Location += {continent}",
                $"$var1 <= \"Location/{continent}\"",
                "/$var1"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }
    }
}