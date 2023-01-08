// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class IdentifierSetTests : IClassFixture<LogicalInfrastructureUnitTestContext>
    {
        private readonly LogicalInfrastructureUnitTestContext _testContext;
        private readonly InfrastructureTestHelper _infrastructureTestHelper = new();

        public IdentifierSetTests(LogicalInfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task IdentifierSet_Get_Current_Head()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
            var storage = await _testContext.Functional.Storages.GetLocal().ConfigureAwait(false);

            // Act.
            var identifier = await _testContext.Logical.Identifiers.GetCurrentHead(storage.Id, space.Id).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(identifier, Identifier.Empty);
        }

        [Fact]
        public async Task IdentifierSet_Get_Next_Head()
        {
	        // Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
            var storage = await _testContext.Functional.Storages.GetLocal().ConfigureAwait(false);

            // Act.
            var head = await _testContext.Logical.Identifiers.GetNextHead(storage.Id, space.Id).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(head.NextHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(head.PreviousHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(head.NextHeadIdentifier, head.PreviousHeadIdentifier);
        }

        [Fact]
        public async Task IdentifierSet_Get_Current_Tail()
        {
			// Arrange.
            var space = await _infrastructureTestHelper.CreateSpace(_testContext.Functional).ConfigureAwait(false);
            var storage = await _testContext.Functional.Storages.GetLocal().ConfigureAwait(false);

            // Act.
            var identifier = await _testContext.Logical.Identifiers.GetTail(storage.Id, space.Id).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(identifier, Identifier.Empty);
        }
    }
}
