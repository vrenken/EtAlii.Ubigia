namespace EtAlii.Servus.Api.Data.Tests.IntegrationTests
{
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimpleInjector;
    using System;
    using System.Net;
    using IInfrastructure = EtAlii.Servus.Api.IInfrastructureClient;

    [TestClass]
    public class Infrastructure_Authentication_Tests : ApiTestBase<ApiTestHostedInfrastructure>
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
        public void Infrastructure_Get_Authentication_Url()
        {
            var credentials = new NetworkCredential(Infrastructure.Configuration.Account, Infrastructure.Configuration.Password);
            string address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Authenticate);
            var token = Infrastructure.Client.Get<string>(address, credentials);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(token));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Infrastructure_Get_Authentication_Url_Invalid_Password()
        {
            // Arrange.
            var credentials = new NetworkCredential(Infrastructure.Configuration.Account, Infrastructure.Configuration.Password + "BAAD");
            string address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Authenticate);

            // Act
            var act = new Action(() =>
            {
                var token = Infrastructure.Client.Get<string>(address, credentials);
            });

            // Assert.
            ExceptionAssert.Throws<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Infrastructure_Get_Authentication_Url_Invalid_Account()
        {
            // Arrange.
            var credentials = new NetworkCredential(Infrastructure.Configuration.Account + "BAAD", Infrastructure.Configuration.Password);
            string address = Infrastructure.AddressFactory.CreateFullAddress(Infrastructure.Configuration.Address, RelativeUri.Authenticate);

            // Act
            var act = new Action(() =>
            {
                var token = Infrastructure.Client.Get<string>(address, credentials);
            });

            // Assert.
            ExceptionAssert.Throws<UnauthorizedInfrastructureOperationException>(act);
        }
    }
}
