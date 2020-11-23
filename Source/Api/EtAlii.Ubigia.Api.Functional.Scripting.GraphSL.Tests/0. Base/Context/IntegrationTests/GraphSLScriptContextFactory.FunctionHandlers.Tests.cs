namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class GraphSLScriptContextFactoryFunctionHandlersTests : IClassFixture<ScriptingUnitTestContext>
    {
        private readonly ScriptingUnitTestContext _testContext;

        public GraphSLScriptContextFactoryFunctionHandlersTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new InvalidTestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                new GraphSLScriptContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Multiple()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestFormatFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);
            
            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Multiple_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestRenameFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(functionHandlersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                new GraphSLScriptContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
