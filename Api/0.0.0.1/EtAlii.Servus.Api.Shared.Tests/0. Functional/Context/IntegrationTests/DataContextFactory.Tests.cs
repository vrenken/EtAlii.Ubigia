namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FunctionalContext_Tests
    {
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataContextFactory_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.IsNotNull(context);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.IsNotNull(context);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(functionHandlersProvider);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.IsNotNull(context);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
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

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Multiple()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
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
            Assert.IsNotNull(context);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_FunctionHandler_Multiple_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
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
