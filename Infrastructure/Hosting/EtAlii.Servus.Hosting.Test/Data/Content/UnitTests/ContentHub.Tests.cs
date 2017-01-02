namespace EtAlii.Servus.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentNotificationHub_Tests 
    {
        [TestMethod]
        public void ContentNotificationHub_Create()
        {
            var contentNotificationHub = new ContentNotificationHub();
            Assert.IsNotNull(contentNotificationHub);
        }
    }
}
