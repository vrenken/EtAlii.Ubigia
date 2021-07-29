// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;

    [Trait("Technology", "Grpc")]
    public class IdentifierRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public IdentifierRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task IdentifierRepository_Get_Current_Head()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);

            var identifier = await context.Host.Infrastructure.Identifiers.GetCurrentHead(space.Id).ConfigureAwait(false);
            Assert.NotEqual(identifier, Identifier.Empty);
        }

        [Fact]
        public async Task IdentifierRepository_Get_Next_Head()
        {
	        // Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);

            var head = await context.Host.Infrastructure.Identifiers.GetNextHead(space.Id).ConfigureAwait(false);
            Assert.NotEqual(head.NextHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(head.PreviousHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(head.NextHeadIdentifier, head.PreviousHeadIdentifier);
        }

        [Fact]
        public async Task IdentifierRepository_Get_Current_Tail()
        {
			// Arrange.
	        var context = _testContext.Host;
            var space = await _infrastructureTestHelper.CreateSpace(context.Host.Infrastructure).ConfigureAwait(false);

            var identifier = await context.Host.Infrastructure.Identifiers.GetTail(space.Id).ConfigureAwait(false);
            Assert.NotEqual(identifier, Identifier.Empty);
        }
    }
}
