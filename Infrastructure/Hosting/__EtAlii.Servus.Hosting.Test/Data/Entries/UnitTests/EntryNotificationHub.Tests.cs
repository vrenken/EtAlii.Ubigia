namespace EtAlii.Servus.Hosting.UnitTests
{
    using EtAlii.Servus.Hosting;
    using EtAlii.Servus.Infrastructure.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EntryNotificationHub_Tests 
    {
        [TestMethod]
        public void EntryNotificationHub_Create()
        {
            var entryNotificationHub = new EntryNotificationHub();
            Assert.IsNotNull(entryNotificationHub);
        }
    }
}
