namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.xTechnology.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public abstract class TestBase<THost> : EtAlii.Servus.Infrastructure.Tests.TestBase
        where THost: class, IHost
    {
        private const string TestConfigurationAccount = "unittest";
        private const string TestConfigurationPassword = "123";
        protected const int TestConfigurationPort = 62000;
        protected const string TestConfigurationAddressFormat = "http://localhost:{0}";

        //protected IInfrastructure Infrastructure { get; private set; }
        protected static THost Host { get; private set; }
        protected static string AccountName { get; private set; }
        protected static string AccountPassword { get; private set; }

        protected static string CreateAddress()
        {
            var address = String.Format(TestConfigurationAddressFormat, TestConfigurationPort);
            return address;
        }

        public static void StartHosting(
            Func
                <IHostingConfiguration, IInfrastructureConfiguration, IStorageConfiguration, IDiagnosticsConfiguration,
                    THost> createHost,
            Func<string> createAddress)
        {
            StartHosting(TestAssembly.StorageName, createHost, createAddress);
        }

        public static void StartHosting(string hostingName, Func<IHostingConfiguration, IInfrastructureConfiguration, IStorageConfiguration, IDiagnosticsConfiguration, THost> createHost, Func<string> createAddress)
        {
            var address = createAddress();
            var hostingConfiguration = CreateHostingConfiguration(address, hostingName);
            var infrastructureConfiguration = CreateInfrastructureFactory(address);
            var storageConfiguration = CreateStorageConfiguration();

            var diagnostics = new DiagnosticsFactory().Create(true, true, true,
                () => new LogFactory(),
                () => new ProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Infrastructure"),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.Infrastructure"));

            Host = createHost(hostingConfiguration, infrastructureConfiguration, storageConfiguration, diagnostics);
            Host.Start();

            AccountName = Host.Configuration.Account;// Guid.NewGuid().ToString();
            AccountPassword = Host.Configuration.Password;// Guid.NewGuid().ToString();
        }

        public static void StopHosting()
        {
            Host.Stop();
            Host = null;
            AccountName = null;
            AccountPassword = null;
        }

        private static StorageConfiguration CreateStorageConfiguration()
        {
            var storageConfiguration = new StorageConfiguration
            {
                Name = "Unit test storage"
            };
            return storageConfiguration;
        }

        private static InfrastructureConfiguration CreateInfrastructureFactory(string address)
        {
            var infrastructureConfiguration = new InfrastructureConfiguration
            {
                Address = address,
                Name = "Unit test infrastructure",
                Account = TestConfigurationAccount,
                Password = TestConfigurationPassword,
            };
            return infrastructureConfiguration;
        }

        private static HostingConfiguration CreateHostingConfiguration(string address, string hostingName)
        {
            var hostingConfiguration = new HostingConfiguration
            {
                Name = hostingName,
                Address = address,
                Account = TestConfigurationAccount,
                Password = TestConfigurationPassword,
            };
            return hostingConfiguration;
        }
    }
}
