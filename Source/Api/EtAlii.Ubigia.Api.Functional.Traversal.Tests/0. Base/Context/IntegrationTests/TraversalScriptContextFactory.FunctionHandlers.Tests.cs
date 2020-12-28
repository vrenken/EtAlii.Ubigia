namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class TraversalScriptContextFactoryFunctionHandlersTests : IClassFixture<ScriptingUnitTestContext>
    {
        private readonly ScriptingUnitTestContext _testContext;

        public TraversalScriptContextFactoryFunctionHandlersTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration()
                    .UseTestParser();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new InvalidTestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                new TraversalScriptContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_FunctionHandler_Multiple()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestFormatFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_FunctionHandler_Multiple_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestRenameFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                new TraversalScriptContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
