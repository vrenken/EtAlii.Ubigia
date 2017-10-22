namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;


    public class DataContextFactoryRootHandlersTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public DataContextFactoryRootHandlersTests(LogicalUnitTestContext testContext)
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
        public async Task DataContextFactory_Create_With_RootHandler_None()
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
        public async Task DataContextFactory_Create_With_RootHandler_Single()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[] { new TestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(rootHandlerMappersProvider);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_RootHandler_Single_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[] { new InvalidTestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(rootHandlerMappersProvider);

            // Act.
            var act = new Action(() =>
            {
                new DataContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_RootHandler_Multiple()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRoot2HandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(rootHandlerMappersProvider);

            // Act.
            var context = new DataContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataContextFactory_Create_With_RootHandler_Multiple_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRootHandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            var configuration = new DataContextConfiguration()
                .Use(logicalContext)
                .Use(rootHandlerMappersProvider);

            // Act.
            var act = new Action(() =>
            {
                new DataContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
