namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.Servus.Api.Shared.Tests;
    using EtAlii.Servus.Infrastructure.Hosts;
    using EtAlii.Servus.Infrastructure.Model;
    using SimpleInjector;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public abstract class InfrastructureTestAppBase : EtAlii.Servus.Infrastructure.Shared.App
    {
        public static int TestConfigurationPort = 62000;
        private string TestConfigurationName;
        public static string TestConfigurationAccount = "unittest";
        public static string TestConfigurationPassword = "123";

        protected InfrastructureTestAppBase(string environmentName)
        {
            TestConfigurationName = environmentName;
        }
        protected override void RegisterTypes()
        {
            base.RegisterTypes();
        }

        protected override IHostingConfiguration GetConfiguration()
        {
            var configuration = new Configuration
            {
                Name = TestConfigurationName,
                Address = String.Format("http://localhost:{0}", TestConfigurationPort),
                Account = TestConfigurationAccount,
                Password = TestConfigurationPassword,
            };
            return configuration;
        }
    }
}
