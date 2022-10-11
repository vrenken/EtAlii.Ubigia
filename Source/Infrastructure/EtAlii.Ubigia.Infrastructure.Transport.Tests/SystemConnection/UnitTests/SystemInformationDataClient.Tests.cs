// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using Xunit;

    public class SystemInformationDataClientTests
    {
        [Fact]
        public void SystemInformationDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemInformationDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
