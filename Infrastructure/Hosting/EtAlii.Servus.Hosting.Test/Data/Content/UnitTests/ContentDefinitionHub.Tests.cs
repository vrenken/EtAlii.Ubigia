namespace EtAlii.Servus.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentDefinitionNotificationHub_Tests 
    {
        [TestMethod]
        public void ContentDefinitionNotificationHub_Create()
        {
            var contentDefinitionNotificationHub = new ContentDefinitionNotificationHub();
            Assert.IsNotNull(contentDefinitionNotificationHub);
        }
    }
}
