// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorRootedPathGetTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathGetTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItem()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "Time:";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariable_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "$var1 <= Time:",
                "$var1"
            };

            var script = _parser.Parse(queries, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Node>(result.Single());
            Assert.Equal("Time", result.Cast<Node>().Single().Type);
        }

        //[Ignore, TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task ScriptProcessor_RootedPath_Get_GetItemByVariable_2()
        //[
        //    // Arrange.
        //    using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true)
        //    var queries = new[]
        //    [
        //        "$var1 <= \"Time\":",
        //        "$var1"
        //    ]
        //    var script = _parser.Parse(queries).Script
        //    var scope = new ExecutionScope()
        //    var options = new ScriptProcessorOptions()
        //        .Use(_diagnostics)
        //        .Use(scope)
        //        .Use(logicalContext)
        //    var processor = _testContext.Create(options)

        //    // Act.
        //    var lastSequence = await processor.Process(script)
        //    var result = await lastSequence.Output.ToArray()

        //    // Assert.
        //    Assert.NotNull(result)
        //    Assert.IsType<DynamicNode>(result.Single())
        //    Assert.Equal("Time", result.Cast<Node>().Single().Type)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_1_Absolute_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";

            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= Location",
                $"$var2 <= {continent}",
                "/$var1/$var2"
            };

            var query = string.Join("\r\n", queries);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_1_Absolute_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= /Location",
                $"$var2 <= {continent}",
                "$var1/$var2"
            };

            var query = string.Join("\r\n", queries);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_1_Rooted()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= Location:",
                $"$var2 <= {continent}",
                "$var1/$var2"
            };

            var query = string.Join("\r\n", queries);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.NotNull(lastSequence.Sequence);
            Assert.NotNull(lastSequence.ExecutionPlan);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_2_Absolute_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_2_Absolute_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= /\"Location\"",
                $"$var2 <= \"{continent}\"",
                "$var1/$var2"
            };

            var script = _parser.Parse(queries, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_Spaced()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location: += {continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<Node>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var now = DateTime.Now;
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time:+={0:yyyy}",
                "$var1 <= Time:{0:yyyy}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal("000", result.Cast<Node>().Single().Type); // A time root query will return 000 milliseconds.
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var now = DateTime.Now;
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "$var1 <= Time:{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fff}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal($"{now:fff}", result.Cast<Node>().Single().Type); // A time root query will return milliseconds.
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var past = DateTime.Now.Subtract(TimeSpan.FromSeconds(5));
            var now = DateTime.Now;
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time:{1:yyyy}{1:MM}{1:dd}{1:HH}{1:mm}{1:ss}{1:fff}", // This should not have any impact.
                "$var1 <= Time:{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fff}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now, past);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal($"{now:fff}", result.Cast<Node>().Single().Type); // A time root query will return milliseconds.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_Spaced_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var now = DateTime.Now;
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time: += {0:yyyy}",
                "$var1 <= Time:{0:yyyy}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal("000", result.Cast<Node>().Single().Type); // A time root query will return milliseconds.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_Spaced_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var now = DateTime.Now;
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time: += {0:yyyy}",
                "$var1 <= Time:{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fff}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal($"{now:fff}", result.Cast<Node>().Single().Type); // A time root query will return milliseconds.
        }
    }
}
