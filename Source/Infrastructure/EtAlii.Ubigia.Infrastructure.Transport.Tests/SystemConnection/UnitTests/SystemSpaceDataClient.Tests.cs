// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using Xunit;

    public class SystemSpaceDataClientTests
    {
        [Fact]
        public void SystemSpaceDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemSpaceDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
