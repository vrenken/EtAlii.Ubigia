namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using Xunit;

    [Trait("Technology", "Grpc")]
    public class IdentifierRepositoryTests : IClassFixture<InfrastructureUnitTestContext>
    {
        private readonly InfrastructureUnitTestContext _testContext;

        public IdentifierRepositoryTests(InfrastructureUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task IdentifierRepository_Get_Current_Head()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            var identifier = context.Host.Infrastructure.Identifiers.GetCurrentHead(space.Id);
            Assert.NotEqual(identifier, Identifier.Empty);
        }

        [Fact]
        public async Task IdentifierRepository_Get_Next_Head()
        {
	        // Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            var identifier = context.Host.Infrastructure.Identifiers.GetNextHead(space.Id, out Identifier previousHeadIdentifier);
            Assert.NotEqual(identifier, Identifier.Empty);
            Assert.NotEqual(previousHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(identifier, previousHeadIdentifier);
        }

        [Fact]
        public async Task IdentifierRepository_Get_Current_Tail()
        {
			// Arrange.
	        var context = _testContext.HostTestContext;
            var space = await InfrastructureTestHelper.CreateSpace(context.Host.Infrastructure);

            var identifier = context.Host.Infrastructure.Identifiers.GetTail(space.Id);
            Assert.NotEqual(identifier, Identifier.Empty);
        }
    }
}
