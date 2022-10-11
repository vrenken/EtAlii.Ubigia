// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using Xunit;

    public class SystemRootDataClientTests
    {
        [Fact]
        public void SystemRootDataClient_Create()
        {
            // Arrange.

            // Act.
            var client = new SystemRootDataClient(null);

            // Assert.
            Assert.NotNull(client);
        }
    }
}
