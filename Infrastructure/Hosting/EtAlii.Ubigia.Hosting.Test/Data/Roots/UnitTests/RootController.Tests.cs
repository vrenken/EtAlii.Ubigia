namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using Xunit;

    
    public class RootController_Tests 
    {
        [Fact]
        public void RootController_Create()
        {
            var rootController = new RootController(null);
            Assert.NotNull(rootController);
        }
    }
}
