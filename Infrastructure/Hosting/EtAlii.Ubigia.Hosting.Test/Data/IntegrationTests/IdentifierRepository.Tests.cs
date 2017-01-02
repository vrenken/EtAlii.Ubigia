namespace EtAlii.Ubigia.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using Xunit;

    
    public class IdentifierRepository_Tests : IClassFixture<HostUnitTestContext>
    {
        private readonly HostUnitTestContext _testContext;

        public IdentifierRepository_Tests(HostUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void IdentifierRepository_Get_Current_Head()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            var identifier = _testContext.HostTestContext.Host.Infrastructure.Identifiers.GetCurrentHead(space.Id);
            Assert.NotEqual(identifier, Identifier.Empty);
        }

        [Fact]
        public void IdentifierRepository_Get_Next_Head()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            Identifier previousHeadIdentifier;
            var identifier = _testContext.HostTestContext.Host.Infrastructure.Identifiers.GetNextHead(space.Id, out previousHeadIdentifier);
            Assert.NotEqual(identifier, Identifier.Empty);
            Assert.NotEqual(previousHeadIdentifier, Identifier.Empty);
            Assert.NotEqual(identifier, previousHeadIdentifier);
        }

        [Fact]
        public void IdentifierRepository_Get_Current_Tail()
        {
            var space = InfrastructureTestHelper.CreateSpace(_testContext.HostTestContext.Host.Infrastructure);

            var identifier = _testContext.HostTestContext.Host.Infrastructure.Identifiers.GetTail(space.Id);
            Assert.NotEqual(identifier, Identifier.Empty);
        }
    }
}
