namespace EtAlii.Servus.Infrastructure.Hosting.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class HostingConfigurationSection_Tests
    {
        [TestMethod]
        public void HostingConfigurationSection_Check_Name()
        {
            var configuration = new HostingConfigurationSection();
            configuration.Name = Guid.NewGuid().ToString();
            Assert.AreEqual(configuration.Name, configuration.Name);
        }

        [TestMethod]
        public void HostingConfigurationSection_Check_Password()
        {
            var configuration = new HostingConfigurationSection();
            configuration.Password = Guid.NewGuid().ToString();
            Assert.AreEqual(configuration.Password, configuration.Password);
        }
    }
}
