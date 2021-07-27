// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public partial class GraphComposerIntegrationTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public GraphComposerIntegrationTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task GraphComposer_Create()
        {
            // Arrange.
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
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
