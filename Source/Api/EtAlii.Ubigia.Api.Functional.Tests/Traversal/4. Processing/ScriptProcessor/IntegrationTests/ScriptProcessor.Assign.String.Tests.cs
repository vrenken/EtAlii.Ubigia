// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorAssignStringUnitTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorAssignStringUnitTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var script = _parser.Parse("$var1 <= \"Time\"", scope).Script;
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            var processor = new ScriptProcessorFactory().Create(options);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            await lastSequence.Output.ToArray();
            // Assert.
            Assert.Equal("Time", await scope.Variables["var1"].Value.SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1", scope).Script;
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            var processor = new ScriptProcessorFactory().Create(options);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Replace()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"Location\"", scope).Script;
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            var processor = new ScriptProcessorFactory().Create(options);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Clear()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <=", scope).Script;
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            var processor = new ScriptProcessorFactory().Create(options);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            await lastSequence.Output.LastOrDefaultAsync();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
            Assert.False(scope.Variables.ContainsKey("var1"));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Empty_String()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"\"", scope).Script;
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            var processor = new ScriptProcessorFactory().Create(options);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
            Assert.Equal("", await scope.Variables["var1"].Value.SingleAsync());
        }
    }
}
