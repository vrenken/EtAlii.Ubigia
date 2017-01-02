using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EtAlii.Servus.PowerShell.Storages;
using EtAlii.Servus.PowerShell.Accounts;
using EtAlii.Servus.PowerShell.Spaces;

namespace EtAlii.Servus.PowerShell.Test
{
    public class TestStatus
    {
        public string Message { get; set; }
        public int Value { get; set; }
    }

    public class GeoIp
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string metro_code { get; set; }
        public string areacode { get; set; }
    }

    public class TestMessage
    {
        public string Name;
        public int Value;
    }

    [TestClass]
    public class Infrastructure_Test
    {
        private string _url = "http://api.openkeyval.org/";


        [TestMethod]
        public void Infrastructure_Post()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var infrastructure = new EtAlii.Servus.Api.Infrastructure();

            var result = infrastructure.Post<TestMessage>(_url + identifier, testMessage);
        }

        [TestMethod]
        public void Infrastructure_Get()
        {
            var infrastructure = new EtAlii.Servus.Api.Infrastructure();

            var result = infrastructure.Get<GeoIp>("http://freegeoip.net/json/www.nu.nl");
            Assert.IsNotNull(result);
            Assert.AreEqual("NL", result.country_code);
            Assert.AreEqual("Netherlands", result.country_name);
            Assert.AreEqual(52.5f, result.latitude);
            Assert.AreEqual(5.75f, result.longitude);
        }

        

        [TestMethod]
        public void Infrastructure_Post_Get_Result()
        {
            var identifier = Guid.NewGuid().ToString().Replace("-", "");
            var testMessage = new TestMessage
            {
                Name = Guid.NewGuid().ToString(),
                Value = new Random().Next(),
            };

            var infrastructure = new EtAlii.Servus.Api.Infrastructure();

            infrastructure.Post<TestMessage>(_url + identifier, testMessage);

            //var result = infrastructure.Get<TestMessage>(_url + identifier);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.Name, testMessage.Name);
            //Assert.AreEqual(result.Value, testMessage.Value);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Infrastructure_Get_Null()
        {
            var infrastructure = new EtAlii.Servus.Api.Infrastructure();

            infrastructure.Get<TestStatus>(null);
        }
    }
}
