namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using Xunit;

    
    public sealed class AccountController_Tests 
    {
        [Fact]
        public void AccountController_Create()
        {
            // Arrange.

            // Act.
            var controller = new AccountController(null);

            // Assert.
            Assert.NotNull(controller);
        }
    }
}
