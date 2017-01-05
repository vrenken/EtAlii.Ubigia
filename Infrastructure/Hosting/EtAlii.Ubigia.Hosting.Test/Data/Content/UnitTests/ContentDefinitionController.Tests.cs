namespace EtAlii.Ubigia.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using Xunit;

    
    public class ContentDefinitionController_Tests 
    {
        [Fact]
        public void ContentDefinitionController_Create()
        {
            var contentDefinitionController = new ContentDefinitionController(null);
            Assert.NotNull(contentDefinitionController);
        }
    }
}
