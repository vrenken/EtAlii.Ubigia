namespace EtAlii.Servus.Hosting.UnitTests
{
    using EtAlii.Servus.Hosting;
    using EtAlii.Servus.Infrastructure.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RootNotificationHub_Tests 
    {
        [TestMethod]
        public void RootNotificationHub_Create()
        {
            var rootNotificationHub = new RootNotificationHub();
            Assert.IsNotNull(rootNotificationHub);
        }
    }
}
