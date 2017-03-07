namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Tests;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;


    public class DataContextFactoryFunctionHandlersTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public DataContextFactoryFunctionHandlersTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[] { new InvalidTestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

            // Act.
            var act = new Action(() =>
            {
                new DataContextFactory().Create(configuration);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Multiple()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestFormatFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Multiple_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestRenameFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

            // Act.
            var act = new Action(() =>
            {
                new DataContextFactory().Create(configuration);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidOperationException>(act);
        }
    }
}
