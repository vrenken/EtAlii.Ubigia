namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using Xunit;

    
    public class PropertiesController_Tests 
    {
        [Fact]
        public void PropertiesController_Create()
        {
            var propertiesController = new PropertiesController(null);
            Assert.NotNull(propertiesController);
        }
    }
}
