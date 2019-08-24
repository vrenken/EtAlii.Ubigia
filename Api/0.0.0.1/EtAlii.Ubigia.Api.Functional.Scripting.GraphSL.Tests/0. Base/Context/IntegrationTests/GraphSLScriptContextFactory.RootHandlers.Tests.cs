namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
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
            var configuration = new GraphSLScriptContextConfiguration();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);
            
            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);
            
            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_None()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Single()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[] { new TestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);
            
            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Single_Invalid()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[] { new InvalidTestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);

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
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRoot2HandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var configuration = new GraphSLScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);

            // Act.
            var scriptContext = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphSLScriptContextFactory_Create_With_RootHandler_Multiple_Invalid()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRootHandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);
            
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);
            
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
