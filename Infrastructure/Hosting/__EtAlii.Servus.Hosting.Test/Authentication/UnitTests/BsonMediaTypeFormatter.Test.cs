namespace EtAlii.Servus.Hosting.UnitTests
{
    using EtAlii.Servus.Infrastructure.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BsonMediaTypeFormatter_Tests
    {
        [TestMethod]
        public void BsonMediaTypeFormatter_Create()
        {
            var formatter = new BsonMediaTypeFormatter();
        }
    }
}
