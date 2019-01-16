namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Xunit;


    public class GraphSLScriptContextFactoryFunctionHandlersTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public GraphSLScriptContextFactoryFunctionHandlersTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[] { new InvalidTestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);
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
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestFormatFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);
            
            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_FunctionHandler_Multiple_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestRenameFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

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
