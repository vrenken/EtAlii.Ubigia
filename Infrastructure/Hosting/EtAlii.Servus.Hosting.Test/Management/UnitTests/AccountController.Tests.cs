namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using Xunit;

    
    public sealed class AccountController_Tests 
    {
        [Fact]
        public void AccountController_Create()
        {
            var controller = new AccountController(null);
        }
    }
}
