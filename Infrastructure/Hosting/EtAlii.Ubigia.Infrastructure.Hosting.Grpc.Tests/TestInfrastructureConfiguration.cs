namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public static class TestInfrastructureConfiguration
    {
        private const int TestPort = 62000;
        private const string TestAddressFormat = "http://localhost:{0}";

        public static IInfrastructureConfiguration Create()
        {
	        var address = new Uri(string.Format(TestAddressFormat, TestPort), UriKind.Absolute);
			var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            return new InfrastructureConfiguration(systemConnectionCreationProxy)
                .Use("Unit test infrastructure", address);
        }
    }
}