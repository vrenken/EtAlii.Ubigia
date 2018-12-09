namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Xunit;

    public class GraphSLScriptContextFactoryRootHandlersTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public GraphSLScriptContextFactoryRootHandlersTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var dataContextConfiguration = new DataContextConfiguration()
                .Use(logicalContext);
            var dataContext = new DataContextFactory().Create(dataContextConfiguration);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(dataContext);
            
            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);
            
            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_None()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var dataContextConfiguration = new DataContextConfiguration()
                .Use(logicalContext);
            var dataContext = new DataContextFactory().Create(dataContextConfiguration);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(dataContext);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Single()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[] { new TestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            var dataContextConfiguration = new DataContextConfiguration()
                .Use(logicalContext);

            var dataContext = new DataContextFactory().Create(dataContextConfiguration);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(dataContext)
                .Use(rootHandlerMappersProvider);
            
            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Single_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[] { new InvalidTestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            var dataContextConfiguration = new DataContextConfiguration()
                .Use(logicalContext);

            var dataContext = new DataContextFactory().Create(dataContextConfiguration);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(dataContext)
                .Use(rootHandlerMappersProvider);

            // Act.
            var act = new Action(() =>
            {
                new GraphSLScriptContextFactory().Create(configuration);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Multiple()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRoot2HandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var dataContextConfiguration = new DataContextConfiguration()
                .Use(logicalContext);

            var dataContext = new DataContextFactory().Create(dataContextConfiguration);
            
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(dataContext)
                .Use(rootHandlerMappersProvider);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Multiple_Invalid()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRootHandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            var dataContextConfiguration = new DataContextConfiguration()
                .Use(logicalContext);

            var dataContext = new DataContextFactory().Create(dataContextConfiguration);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(dataContext)
                .Use(rootHandlerMappersProvider);
            
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
