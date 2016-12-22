namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using Xunit;

    
    public class EntryController_Tests 
    {
        [Fact]
        public void EntryController_Create()
        {
            var entryController = new EntryController(null);
            Assert.NotNull(entryController);
        }
    }
}
