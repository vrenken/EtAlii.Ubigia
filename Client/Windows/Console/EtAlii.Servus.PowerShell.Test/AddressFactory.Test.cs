namespace EtAlii.Servus.PowerShell.Test
{
    using EtAlii.Servus.Api;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class AddressFactory_Test
    {
        private const string baseAddress = "http://localtesthost:1234";
        private AddressFactory factory;
        private EtAlii.Servus.Client.Model.Storage storage;

        [TestInitialize]
        public void Initialize()
        {
            factory = new AddressFactory();
            storage = new EtAlii.Servus.Client.Model.Storage { Address = baseAddress };
        }

        [TestCleanup]
        public void Cleanup()
        {
            factory = null;
            storage = null;
        }

        [TestMethod]
        public void AddressFactory_Create()
        {
            string address = factory.Create(storage, "test");
            Assert.AreEqual(address, baseAddress + "/test");
        }

        [TestMethod]
        public void AddressFactory_Create_With_Parameters()
        {
            string address = factory.Create(storage, "test", "firstkey", "firstvalue", "secondKey", "secondvalue");
            Assert.AreEqual(address, baseAddress + "/test?firstkey=firstvalue&secondKey=secondvalue");
        }

        [TestMethod]
        public void AddressFactory_Create_With_Special_Parameters()
        {
            string address = factory.Create(storage, "test", "firstkey", "first=value", "secondKey", "second&value");
            Assert.AreEqual(address, baseAddress + "/test?firstkey=first%3Dvalue&secondKey=second%26value");
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void AddressFactory_Create_Storage_Is_Null()
        {
            factory.Create(null, "test");
        }

        [TestMethod]
        public void AddressFactory_Create_Path_Is_Null()
        {
            var address = factory.Create(storage, null);
            Assert.AreEqual(address, baseAddress + "/");
        }
    }
}
