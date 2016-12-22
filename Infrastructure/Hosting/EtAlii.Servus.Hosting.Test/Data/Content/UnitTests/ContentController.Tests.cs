namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using Xunit;

    
    public class ContentController_Tests 
    {
        [Fact]
        public void ContentController_Create()
        {
            var contentController = new ContentController(null);
            Assert.NotNull(contentController);
        }
    }
}
