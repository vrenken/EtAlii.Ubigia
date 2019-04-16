namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using Xunit;

    
    public partial class GraphComposerIntegrationTests : IClassFixture<FabricUnitTestContext>
    {
        private readonly FabricUnitTestContext _testContext;

        public GraphComposerIntegrationTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task GraphComposer_Create()
        {
            // Arrange.
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var graphPathTraverserConfiguration = new GraphPathTraverserConfiguration().Use(fabric);
            var graphPathTraverserFactory = new GraphPathTraverserFactory();
            var graphPathTraverser = graphPathTraverserFactory.Create(graphPathTraverserConfiguration);
            
            // Act.
            var composer = new GraphComposerFactory(graphPathTraverser).Create(fabric);

            // Assert.
            Assert.NotNull(composer);
        }
    }
}