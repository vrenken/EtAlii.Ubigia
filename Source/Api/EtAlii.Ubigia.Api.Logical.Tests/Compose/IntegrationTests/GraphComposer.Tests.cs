// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
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
            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration).Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);

            // Act.
            var composer = new GraphComposerFactory(traverser).Create(fabric);

            // Assert.
            Assert.NotNull(composer);
        }
    }
}
