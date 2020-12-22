namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class TraversalScriptContextFactoryRootHandlersTests : IClassFixture<ScriptingUnitTestContext>
    {
        private readonly ScriptingUnitTestContext _testContext;

        public TraversalScriptContextFactoryRootHandlersTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_RootHandler_None()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_RootHandler_Single()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[] { new TestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var configuration = new TraversalScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_RootHandler_Single_Invalid()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[] { new InvalidTestRootHandlerMapper() };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var configuration = new TraversalScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
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
        public async Task TraversalScriptContextFactory_Create_With_RootHandler_Multiple()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRoot2HandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var configuration = new TraversalScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalScriptContextFactory_Create_With_RootHandler_Multiple_Invalid()
        {
            // Arrange.
            var rootHandlerMappers = new IRootHandlerMapper[]
            {
                new TestRootHandlerMapper(),
                new TestRootHandlerMapper()
            };
            var rootHandlerMappersProvider = new RootHandlerMappersProvider(rootHandlerMappers);

            var configuration = new TraversalScriptContextConfiguration()
                .Use(rootHandlerMappersProvider);
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
