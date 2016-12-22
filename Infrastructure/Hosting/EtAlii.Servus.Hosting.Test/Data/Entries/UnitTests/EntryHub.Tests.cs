namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.SignalR;
    using Xunit;

    
    public class EntryHub_Tests 
    {
        [Fact]
        public void EntryHub_Create()
        {
            var entryHub = new EntryHub(null, null);
            Assert.NotNull(entryHub);
        }
    }
}
