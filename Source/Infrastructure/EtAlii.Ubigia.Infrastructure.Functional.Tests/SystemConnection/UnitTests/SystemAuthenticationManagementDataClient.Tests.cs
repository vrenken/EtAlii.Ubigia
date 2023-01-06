// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using Xunit;

    public class SystemAuthenticationManagementDataClientTests
    {
        [Fact]
        public void SystemAuthenticationManagementDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemAuthenticationManagementDataClient();

            // Assert.
            Assert.NotNull(client);
        }
    }
}
