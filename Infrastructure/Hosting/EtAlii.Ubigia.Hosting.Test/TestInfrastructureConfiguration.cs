namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public static class TestInfrastructureConfiguration
    {
        private const string TestAccount = "System";
        private const string TestPassword = "system123";
        private const int TestPort = 62000;
        private const string TestAddressFormat = "http://localhost:{0}";

        public static IInfrastructureConfiguration Create()
        {
            var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            return new InfrastructureConfiguration(systemConnectionCreationProxy)
                .Use("Unit test infrastructure", String.Format(TestAddressFormat, TestPort), TestAccount, TestPassword);
        }
    }
}