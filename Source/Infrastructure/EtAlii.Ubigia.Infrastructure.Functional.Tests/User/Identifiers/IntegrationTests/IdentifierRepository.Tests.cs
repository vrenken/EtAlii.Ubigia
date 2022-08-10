// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class IdentifierRepositoryTests : IClassFixture<FunctionalInfrastructureUnitTestContext>
    {
        private readonly FunctionalInfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public IdentifierRepositoryTests(FunctionalInfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task IdentifierRepository_Get_Current_Head()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Infrastructure).ConfigureAwait(false);

            var identifier = await _testContext.Infrastructure.Identifiers.GetCurrentHead(space.Id).ConfigureAwait(false);
            Assert.NotEqual(identifier, Identifier.Empty);
        }

        [Fact]
        public async Task IdentifierRepository_Get_Next_Head()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Infrastructure).ConfigureAwait(false);

            var head = await _testContext.Infrastructure.Identifiers.GetNextHead(space.Id).ConfigureAwait(false);
            Assert.NotEqual(head.NextHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(head.PreviousHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(head.NextHeadIdentifier, head.PreviousHeadIdentifier);
        }

        [Fact]
        public async Task IdentifierRepository_Get_Current_Tail()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Infrastructure).ConfigureAwait(false);

            var identifier = await _testContext.Infrastructure.Identifiers.GetTail(space.Id).ConfigureAwait(false);
            Assert.NotEqual(identifier, Identifier.Empty);
        }
    }
}
