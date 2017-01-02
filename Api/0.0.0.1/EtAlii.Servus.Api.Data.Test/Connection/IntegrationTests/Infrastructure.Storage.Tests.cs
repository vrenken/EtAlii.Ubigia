namespace EtAlii.Servus.Api.Data.Tests.IntegrationTests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimpleInjector;
    using System;
    using System.Net;
    using Storage = EtAlii.Servus.Api.Storage;
    using IInfrastructure = EtAlii.Servus.Api.IInfrastructureClient;

    [TestClass]
    public class Infrastructure_Storage_Tests : ApiTestBase<ApiTestHostedInfrastructure>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Local_With_Authentication()
        {
            var credentials = new NetworkCredential(Infrastructure.Configuration.Account, Infrastructure.Configuration.Password);
            string address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Authenticate);
            var token = Infrastructure.Client.Get<string>(address, credentials);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(token));

            Infrastructure.Client.AuthenticationToken = token;

            address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Storages) + "?local";
            var storage = Infrastructure.Client.Get<Storage>(address);
            Assert.IsNotNull(storage);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Local_Without_Authentication()
        {
            var address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Storages) + "?local";
            var storage = Infrastructure.Client.Get<Storage>(address);
            Assert.IsNotNull(storage);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Infrastructure_Get_Storage_Delayed()
        {
            System.Threading.Thread.Sleep(50000);
            var address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Storages) + "?local";
            var storage = Infrastructure.Client.Get<Storage>(address);
            Assert.IsNotNull(storage);
        }
    }
}
